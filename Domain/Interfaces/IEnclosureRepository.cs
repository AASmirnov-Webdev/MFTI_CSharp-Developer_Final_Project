using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEnclosureRepository
    {
        Task<Enclosure?> GetByIdAsync(Guid id);
        Task<List<Enclosure>> GetAllAsync();
        Task AddAsync(Enclosure enclosure);
        Task UpdateAsync(Enclosure enclosure);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Enclosure>> GetFreeEnclosuresAsync();
    }
}
