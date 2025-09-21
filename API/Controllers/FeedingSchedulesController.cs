using Application.DTOs;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly FeedingOrganizationService _feedingService;
        private readonly IFeedingScheduleRepository _feedingScheduleRepository;

        public FeedingSchedulesController(
            FeedingOrganizationService feedingService,
            IFeedingScheduleRepository feedingScheduleRepository)
        {
            _feedingService = feedingService;
            _feedingScheduleRepository = feedingScheduleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schedules = await _feedingScheduleRepository.GetAllAsync();
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedingScheduleRequest request)
        {
            try
            {
                var schedule = await _feedingService.ScheduleFeedingAsync(
                    request.AnimalId, request.FeedingTime, request.FoodType);
                return CreatedAtAction(nameof(GetById), new { id = schedule.Id }, schedule);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private object GetById()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            try
            {
                await _feedingService.MarkFeedingAsCompletedAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingFeedings([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var feedings = await _feedingService.GetUpcomingFeedingsAsync(from, to);
            return Ok(feedings);
        }
    }
}
