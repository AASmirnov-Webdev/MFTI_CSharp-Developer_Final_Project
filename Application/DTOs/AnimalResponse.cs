using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AnimalResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("species")]
        public string Species { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("favoriteFood")]
        public string FavoriteFood { get; set; } = string.Empty;

        [JsonPropertyName("isHealthy")]
        public bool IsHealthy { get; set; }

        [JsonPropertyName("enclosureId")]
        public Guid EnclosureId { get; set; }
    }
}
