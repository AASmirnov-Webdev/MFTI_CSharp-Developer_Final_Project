using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AnimalTransferService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;

        public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }

        public async Task TransferAnimal(Guid animalId, Guid targetEnclosureId)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);
            if (animal == null)
                throw new InvalidOperationException($"Животное с идентификатором {animalId} не найдено!");

            var currentEnclosure = await _enclosureRepository.GetByIdAsync(animal.EnclosureId);
            var targetEnclosure = await _enclosureRepository.GetByIdAsync(targetEnclosureId);

            if (targetEnclosure == null)
                throw new InvalidOperationException($"Вольер с идентификатором {targetEnclosureId} не найден!");

            if (!targetEnclosure.CanAddAnimal())
                throw new InvalidOperationException("Вольер заполнен!");

            currentEnclosure?.RemoveAnimal();
            targetEnclosure.AddAnimal();
            animal.MoveToEnclosure(targetEnclosureId);

            await _animalRepository.UpdateAsync(animal);
            if (currentEnclosure != null)
                await _enclosureRepository.UpdateAsync(currentEnclosure);
            await _enclosureRepository.UpdateAsync(targetEnclosure);
        }
    }
}
