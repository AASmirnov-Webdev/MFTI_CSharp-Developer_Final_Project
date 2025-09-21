using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Animal : EntityBase
    {
        [JsonPropertyName("species")]
        public string Species { get; private set; }

        [JsonPropertyName("name")]
        public string Name { get; private set; }

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; private set; }

        [JsonPropertyName("gender")]
        public string Gender { get; private set; }

        [JsonPropertyName("favoriteFood")]
        public string FavoriteFood { get; private set; }

        [JsonPropertyName("isHealthy")]
        public bool IsHealthy { get; private set; }

        [JsonPropertyName("enclosureId")]
        public Guid EnclosureId { get; private set; }

        public Animal(string species, string name, DateTime birthDate, string gender, string favoriteFood)
        {
            Species = species;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            IsHealthy = true;
        }

        public void Feed()
        {
            var animalFedEvent = new AnimalFedEvent(Id);
            AddDomainEvent(animalFedEvent);
        }

        public void Heal() => IsHealthy = true;

        public void MoveToEnclosure(Guid enclosureId)
        {
            var animalMovedEvent = new AnimalMovedEvent(Id, EnclosureId, enclosureId);
            AddDomainEvent(animalMovedEvent);
            EnclosureId = enclosureId;
        }

        // Метод для тестирования - убирал JsonIgnore, так как это метод, а не свойство
        public void SetHealthStatusForTesting(bool isHealthy)
        {
            IsHealthy = isHealthy;
        }
    }
}
