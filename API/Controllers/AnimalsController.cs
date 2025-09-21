using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ZooManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly AnimalTransferService _animalTransferService;

    public AnimalsController(IAnimalRepository animalRepository, AnimalTransferService animalTransferService)
    {
        _animalRepository = animalRepository;
        _animalTransferService = animalTransferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalRepository.GetAllAsync();

        // Преобразуем доменные сущности в DTO
        var animalResponses = animals.Select(animal => new AnimalResponse
        {
            Id = animal.Id,
            Species = animal.Species,
            Name = animal.Name,
            BirthDate = animal.BirthDate,
            Gender = animal.Gender,
            FavoriteFood = animal.FavoriteFood,
            IsHealthy = animal.IsHealthy,
            EnclosureId = animal.EnclosureId
        }).ToList();

        return Ok(animalResponses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal == null) return NotFound();

        // Преобразуем доменную сущность в DTO
        var animalResponse = new AnimalResponse
        {
            Id = animal.Id,
            Species = animal.Species,
            Name = animal.Name,
            BirthDate = animal.BirthDate,
            Gender = animal.Gender,
            FavoriteFood = animal.FavoriteFood,
            IsHealthy = animal.IsHealthy,
            EnclosureId = animal.EnclosureId
        };

        return Ok(animalResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
    {
        var animal = new Animal(request.Species, request.Name, request.BirthDate,
                              request.Gender, request.FavoriteFood);

        await _animalRepository.AddAsync(animal);

        // Возвращаем DTO вместо доменной сущности
        var animalResponse = new AnimalResponse
        {
            Id = animal.Id,
            Species = animal.Species,
            Name = animal.Name,
            BirthDate = animal.BirthDate,
            Gender = animal.Gender,
            FavoriteFood = animal.FavoriteFood,
            IsHealthy = animal.IsHealthy,
            EnclosureId = animal.EnclosureId
        };

        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animalResponse);
    }

    [HttpPost("{id:guid}/transfer/{enclosureId:guid}")]
    public async Task<IActionResult> TransferAnimal(Guid id, Guid enclosureId)
    {
        try
        {
            await _animalTransferService.TransferAnimal(id, enclosureId);
            return Ok(new { message = "Животное успешно перемещено!" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}