using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ZooStatisticsService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;

        public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }

        public async Task<ZooStatistics> GetStatistics()
        {
            var animals = await _animalRepository.GetAllAsync();
            var enclosures = await _enclosureRepository.GetAllAsync();

            return new ZooStatistics
            {
                TotalAnimals = animals.Count,
                TotalEnclosures = enclosures.Count,
                FreeEnclosures = enclosures.Count(e => e.CurrentCapacity == 0),
                OccupiedEnclosures = enclosures.Count(e => e.CurrentCapacity > 0),
                FullEnclosures = enclosures.Count(e => e.CurrentCapacity >= e.MaxCapacity),
                SickAnimals = animals.Count(a => !a.IsHealthy),
                HealthyAnimals = animals.Count(a => a.IsHealthy)
            };
        }
    }

    public class ZooStatistics
    {
        public int TotalAnimals { get; set; }
        public int TotalEnclosures { get; set; }
        public int FreeEnclosures { get; set; }
        public int OccupiedEnclosures { get; set; }
        public int FullEnclosures { get; set; }
        public int SickAnimals { get; set; }
        public int HealthyAnimals { get; set; }
    }
}
