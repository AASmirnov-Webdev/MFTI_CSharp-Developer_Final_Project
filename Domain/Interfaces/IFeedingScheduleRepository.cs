using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFeedingScheduleRepository
    {
        Task<FeedingSchedule?> GetByIdAsync(Guid id);
        Task<List<FeedingSchedule>> GetAllAsync();
        Task<List<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId);
        Task<List<FeedingSchedule>> GetUpcomingFeedingsAsync(DateTime fromTime);
        Task<List<FeedingSchedule>> GetOverdueFeedingsAsync();
        Task AddAsync(FeedingSchedule feedingSchedule);
        Task UpdateAsync(FeedingSchedule feedingSchedule);
        Task DeleteAsync(Guid id);
    }
}
