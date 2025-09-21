using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EnclosureResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public string Size { get; set; } = string.Empty;

        [JsonPropertyName("currentCapacity")]
        public int CurrentCapacity { get; set; }

        [JsonPropertyName("maxCapacity")]
        public int MaxCapacity { get; set; }

        [JsonPropertyName("isClean")]
        public bool IsClean { get; set; }
    }
}
