using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateEnclosureRequest
    {
        public string Type { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int MaxCapacity { get; set; }
    }
}
