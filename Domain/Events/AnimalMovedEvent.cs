using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class AnimalMovedEvent : DomainEvent
    {
        public Guid AnimalId { get; }
        public Guid FromEnclosureId { get; }
        public Guid ToEnclosureId { get; }

        public AnimalMovedEvent(Guid animalId, Guid fromEnclosureId, Guid toEnclosureId)
        {
            AnimalId = animalId;
            FromEnclosureId = fromEnclosureId;
            ToEnclosureId = toEnclosureId;
        }
    }
}
