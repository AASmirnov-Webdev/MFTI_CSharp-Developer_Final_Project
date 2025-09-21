using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly List<Animal> _animals = new();

        public InMemoryAnimalRepository()
        {
            // Тестовые данные
            var lion = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");
            var tiger = new Animal("Тигр", "Шерхан", new DateTime(2019, 5, 20), "Самец", "Мясо");
            var giraffe = new Animal("Жираф", "Мелман", new DateTime(1985, 2, 11), "Самец", "Листья");

            _animals.Add(lion);
            _animals.Add(tiger);
            _animals.Add(giraffe);
        }

        public Task<Animal?> GetByIdAsync(Guid id)
            => Task.FromResult(_animals.FirstOrDefault(a => a.Id == id));

        public Task<List<Animal>> GetAllAsync()
            => Task.FromResult(_animals.ToList());

        public Task AddAsync(Animal animal)
        {
            _animals.Add(animal);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Animal animal)
        {
            var index = _animals.FindIndex(a => a.Id == animal.Id);
            if (index >= 0) _animals[index] = animal;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _animals.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(Guid id)
            => Task.FromResult(_animals.Any(a => a.Id == id));
    }
}
