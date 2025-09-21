using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryEnclosureRepository : IEnclosureRepository
    {
        private readonly List<Enclosure> _enclosures = new();

        public InMemoryEnclosureRepository()
        {
            // Тестовые данные
            var predatorEnclosure = new Enclosure("Для хищников", "Большой", 3);
            var herbivoreEnclosure = new Enclosure("Для травоядных", "Средний", 5);
            var birdEnclosure = new Enclosure("Для птиц", "Маленький", 10);

            _enclosures.Add(predatorEnclosure);
            _enclosures.Add(herbivoreEnclosure);
            _enclosures.Add(birdEnclosure);
        }

        public Task<Enclosure?> GetByIdAsync(Guid id)
            => Task.FromResult(_enclosures.FirstOrDefault(e => e.Id == id));

        public Task<List<Enclosure>> GetAllAsync()
            => Task.FromResult(_enclosures.ToList());

        public Task AddAsync(Enclosure enclosure)
        {
            _enclosures.Add(enclosure);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Enclosure enclosure)
        {
            var index = _enclosures.FindIndex(e => e.Id == enclosure.Id);
            if (index >= 0) _enclosures[index] = enclosure;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _enclosures.RemoveAll(e => e.Id == id);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(Guid id)
            => Task.FromResult(_enclosures.Any(e => e.Id == id));

        public Task<List<Enclosure>> GetFreeEnclosuresAsync()
            => Task.FromResult(_enclosures.Where(e => e.CurrentCapacity < e.MaxCapacity).ToList());
    }
}
