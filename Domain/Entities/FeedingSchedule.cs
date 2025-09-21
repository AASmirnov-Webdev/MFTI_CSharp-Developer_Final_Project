using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FeedingSchedule : EntityBase
    {
        [JsonPropertyName("animalId")]
        public Guid AnimalId { get; private set; }

        [JsonPropertyName("feedingTime")]
        public DateTime FeedingTime { get; private set; }

        [JsonPropertyName("foodType")]
        public string FoodType { get; private set; }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; private set; }

        public FeedingSchedule(Guid animalId, DateTime feedingTime, string foodType)
        {
            AnimalId = animalId;
            FeedingTime = feedingTime;
            FoodType = foodType;
            IsCompleted = false;
        }

        public void MarkAsCompleted() => IsCompleted = true;
        public void Reschedule(DateTime newTime) => FeedingTime = newTime;

        public void AddDomainEvent(FeedingCompletedEvent feedingCompletedEvent)
        {
            throw new NotImplementedException();
        }

        public void AddDomainEvent(FeedingTimeEvent feedingTimeEvent)
        {
            throw new NotImplementedException();
        }
    }
}
