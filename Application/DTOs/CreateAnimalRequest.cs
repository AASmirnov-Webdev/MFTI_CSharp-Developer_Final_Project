using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateAnimalRequest
    {
        public string Species { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string FavoriteFood { get; set; } = string.Empty;
        public Guid EnclosureId { get; set; }
    }
}
