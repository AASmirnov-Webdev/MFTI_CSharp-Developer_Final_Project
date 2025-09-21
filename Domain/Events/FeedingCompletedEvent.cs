using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class FeedingCompletedEvent : DomainEvent
    {
        public Guid FeedingScheduleId { get; }
        public Guid AnimalId { get; }

        public FeedingCompletedEvent(Guid feedingScheduleId, Guid animalId)
        {
            FeedingScheduleId = feedingScheduleId;
            AnimalId = animalId;
        }
    }
}
