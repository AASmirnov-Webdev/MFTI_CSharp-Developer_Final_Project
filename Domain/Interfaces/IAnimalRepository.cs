using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAnimalRepository
    {
        Task<Animal?> GetByIdAsync(Guid id);
        Task<List<Animal>> GetAllAsync();
        Task AddAsync(Animal animal);
        Task UpdateAsync(Animal animal);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
