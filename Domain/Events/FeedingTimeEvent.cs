using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class FeedingTimeEvent : DomainEvent
    {
        public Guid AnimalId { get; }
        public DateTime FeedingTime { get; }

        public FeedingTimeEvent(Guid animalId, DateTime feedingTime)
        {
            AnimalId = animalId;
            FeedingTime = feedingTime;
        }
    }
}
