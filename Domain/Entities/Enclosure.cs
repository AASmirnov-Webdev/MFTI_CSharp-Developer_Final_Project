using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enclosure : EntityBase
    {
        [JsonPropertyName("type")]
        public string Type { get; private set; }

        [JsonPropertyName("size")]
        public string Size { get; private set; }

        [JsonPropertyName("currentCapacity")]
        public int CurrentCapacity { get; private set; }

        [JsonPropertyName("maxCapacity")]
        public int MaxCapacity { get; private set; }

        [JsonPropertyName("isClean")]
        public bool IsClean { get; private set; }

        public Enclosure(string type, string size, int maxCapacity)
        {
            Type = type;
            Size = size;
            MaxCapacity = maxCapacity;
            CurrentCapacity = 0;
            IsClean = true;
        }

        public bool CanAddAnimal() => CurrentCapacity < MaxCapacity;

        public void AddAnimal()
        {
            if (!CanAddAnimal())
                throw new InvalidOperationException("Enclosure is full");

            CurrentCapacity++;
        }

        public void RemoveAnimal() => CurrentCapacity--;
        public void Clean() => IsClean = true;

        // Метод для тестирования - убрал JsonIgnore
        public void SetCurrentCapacityForTesting(int capacity)
        {
            CurrentCapacity = capacity;
        }
    }
}
