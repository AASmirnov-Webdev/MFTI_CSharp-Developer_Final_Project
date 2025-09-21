using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FeedingOrganizationService
    {
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;
        private readonly IAnimalRepository _animalRepository;

        public FeedingOrganizationService(
            IFeedingScheduleRepository feedingScheduleRepository,
            IAnimalRepository animalRepository)
        {
            _feedingScheduleRepository = feedingScheduleRepository;
            _animalRepository = animalRepository;
        }

        public async Task<FeedingSchedule> ScheduleFeedingAsync(Guid animalId, DateTime feedingTime, string foodType)
        {
            var animal = await _animalRepository.GetByIdAsync(animalId);
            if (animal == null)
                throw new InvalidOperationException($"Животное с идентификатором {animalId} не найдено!");

            var feedingSchedule = new FeedingSchedule(animalId, feedingTime, foodType);
            await _feedingScheduleRepository.AddAsync(feedingSchedule);

            return feedingSchedule;
        }

        public async Task MarkFeedingAsCompletedAsync(Guid feedingScheduleId)
        {
            var feedingSchedule = await _feedingScheduleRepository.GetByIdAsync(feedingScheduleId);
            if (feedingSchedule == null)
                throw new InvalidOperationException($"Расписание кормления с идентификатором {feedingScheduleId} не найдено!");

            feedingSchedule.MarkAsCompleted();
            await _feedingScheduleRepository.UpdateAsync(feedingSchedule);

            feedingSchedule.AddDomainEvent(new FeedingCompletedEvent(feedingScheduleId, feedingSchedule.AnimalId));
        }

        public async Task RescheduleFeedingAsync(Guid feedingScheduleId, DateTime newFeedingTime)
        {
            var feedingSchedule = await _feedingScheduleRepository.GetByIdAsync(feedingScheduleId);
            if (feedingSchedule == null)
                throw new InvalidOperationException($"Расписание кормления с идентификатором {feedingScheduleId} не найдено!");

            feedingSchedule.Reschedule(newFeedingTime);
            await _feedingScheduleRepository.UpdateAsync(feedingSchedule);
        }

        public async Task<List<FeedingSchedule>> GetUpcomingFeedingsAsync(DateTime fromTime, DateTime toTime)
        {
            var allFeedings = await _feedingScheduleRepository.GetAllAsync();
            return allFeedings
                .Where(f => f.FeedingTime >= fromTime && f.FeedingTime <= toTime && !f.IsCompleted)
                .OrderBy(f => f.FeedingTime)
                .ToList();
        }

        public async Task<List<FeedingSchedule>> GetAnimalFeedingsAsync(Guid animalId)
        {
            return await _feedingScheduleRepository.GetByAnimalIdAsync(animalId);
        }

        public async Task DeleteFeedingScheduleAsync(Guid feedingScheduleId)
        {
            var feedingSchedule = await _feedingScheduleRepository.GetByIdAsync(feedingScheduleId);
            if (feedingSchedule == null)
                throw new InvalidOperationException($"Расписание кормления с идентификатором {feedingScheduleId} не найдено!");

            await _feedingScheduleRepository.DeleteAsync(feedingScheduleId);
        }

        public async Task CheckFeedingTimesAsync()
        {
            var currentTime = DateTime.Now;
            var upcomingFeedings = await _feedingScheduleRepository.GetUpcomingFeedingsAsync(currentTime);

            foreach (var feeding in upcomingFeedings.Where(f => f.FeedingTime <= currentTime.AddMinutes(15) && !f.IsCompleted))
            {
                feeding.AddDomainEvent(new FeedingTimeEvent(feeding.AnimalId, feeding.FeedingTime));
                await _feedingScheduleRepository.UpdateAsync(feeding);
            }
        }

        public async Task<FeedingStatistics> GetFeedingStatisticsAsync()
        {
            var allFeedings = await _feedingScheduleRepository.GetAllAsync();
            var completedFeedings = allFeedings.Count(f => f.IsCompleted);
            var pendingFeedings = allFeedings.Count(f => !f.IsCompleted);
            var overdueFeedings = allFeedings.Count(f => !f.IsCompleted && f.FeedingTime < DateTime.Now);

            return new FeedingStatistics
            {
                TotalFeedings = allFeedings.Count,
                CompletedFeedings = completedFeedings,
                PendingFeedings = pendingFeedings,
                OverdueFeedings = overdueFeedings
            };
        }
    }

    public class FeedingStatistics
    {
        public int TotalFeedings { get; set; }
        public int CompletedFeedings { get; set; }
        public int PendingFeedings { get; set; }
        public int OverdueFeedings { get; set; }
        public double CompletionRate => TotalFeedings > 0 ? (CompletedFeedings * 100.0) / TotalFeedings : 0;
    }
}
