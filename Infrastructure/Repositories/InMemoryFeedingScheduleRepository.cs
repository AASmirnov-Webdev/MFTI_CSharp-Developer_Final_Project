using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly List<FeedingSchedule> _feedingSchedules = new();

        public Task<FeedingSchedule?> GetByIdAsync(Guid id)
            => Task.FromResult(_feedingSchedules.FirstOrDefault(f => f.Id == id));

        public Task<List<FeedingSchedule>> GetAllAsync()
            => Task.FromResult(_feedingSchedules.ToList());

        public Task<List<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId)
            => Task.FromResult(_feedingSchedules
                .Where(f => f.AnimalId == animalId)
                .OrderBy(f => f.FeedingTime)
                .ToList());

        public Task<List<FeedingSchedule>> GetUpcomingFeedingsAsync(DateTime fromTime)
            => Task.FromResult(_feedingSchedules
                .Where(f => f.FeedingTime >= fromTime && !f.IsCompleted)
                .OrderBy(f => f.FeedingTime)
                .ToList());

        public Task<List<FeedingSchedule>> GetOverdueFeedingsAsync()
            => Task.FromResult(_feedingSchedules
                .Where(f => f.FeedingTime < DateTime.Now && !f.IsCompleted)
                .OrderBy(f => f.FeedingTime)
                .ToList());

        public Task AddAsync(FeedingSchedule feedingSchedule)
        {
            _feedingSchedules.Add(feedingSchedule);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(FeedingSchedule feedingSchedule)
        {
            var index = _feedingSchedules.FindIndex(f => f.Id == feedingSchedule.Id);
            if (index >= 0) _feedingSchedules[index] = feedingSchedule;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _feedingSchedules.RemoveAll(f => f.Id == id);
            return Task.CompletedTask;
        }
    }
}
