using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateFeedingScheduleRequest
    {
        public Guid AnimalId { get; set; }
        public DateTime FeedingTime { get; set; }
        public string FoodType { get; set; } = string.Empty;
    }
}
