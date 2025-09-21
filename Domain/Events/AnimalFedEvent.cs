using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class AnimalFedEvent : DomainEvent
    {
        public Guid AnimalId { get; }

        public AnimalFedEvent(Guid animalId)
        {
            AnimalId = animalId;
        }
    }
}
