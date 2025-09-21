using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly ZooStatisticsService _statisticsService;
        private readonly FeedingOrganizationService _feedingService;

        public StatisticsController(
            ZooStatisticsService statisticsService,
            FeedingOrganizationService feedingService)
        {
            _statisticsService = statisticsService;
            _feedingService = feedingService;
        }

        [HttpGet("zoo")]
        public async Task<IActionResult> GetZooStatistics()
        {
            var statistics = await _statisticsService.GetStatistics();
            return Ok(statistics);
        }

        [HttpGet("feeding")]
        public async Task<IActionResult> GetFeedingStatistics()
        {
            var statistics = await _feedingService.GetFeedingStatisticsAsync();
            return Ok(statistics);
        }
    }
}
