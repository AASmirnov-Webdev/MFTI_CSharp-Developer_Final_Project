using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class EntityBase
    {
        [JsonPropertyName("id")]
        public Guid Id { get; protected set; }

        private readonly List<DomainEvent> _domainEvents = new();

        // Убирал JsonIgnore отсюда, так как это свойство только для чтения
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntityBase other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (Id == Guid.Empty || other.Id == Guid.Empty) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(EntityBase? a, EntityBase? b) =>
            a is null && b is null ? true : a is null || b is null ? false : a.Equals(b);

        public static bool operator !=(EntityBase? a, EntityBase? b) => !(a == b);
    }
}
