using Application.Services;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ZooManagement.Tests;

public class UnitTest
{
    public bool IsHealthy { get; private set; }
    public int CurrentCapacity { get; private set; }

    // Тест 1. Проверка генерации события кормления
    [Fact]
    public void Animal_Feed_Should_Raise_AnimalFedEvent()
    {
        // Arrange
        var animal = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");

        // Act
        animal.Feed();

        // Assert
        animal.DomainEvents.Should().HaveCount(1);
        animal.DomainEvents.First().Should().BeOfType<AnimalFedEvent>();
    }

    // Тест 2. Проверка лечения животного
    [Fact]
    public void Animal_Heal_Should_Set_IsHealthy_To_True()
    {
        // Arrange
        var animal = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");

        // Act
        animal.Heal();

        // Assert
        animal.IsHealthy.Should().BeTrue();
    }

    // Тест 3. Проверка добавления животного в вольер
    [Fact]
    public void Enclosure_AddAnimal_Should_Increase_CurrentCapacity()
    {
        // Arrange
        var enclosure = new Enclosure("Для хищников", "Большой", 3);
        var initialCapacity = enclosure.CurrentCapacity;

        // Act
        enclosure.AddAnimal();

        // Assert
        enclosure.CurrentCapacity.Should().Be(initialCapacity + 1);
    }

    // Тест 4. Проверка обработки переполнения вольера
    [Fact]
    public void Enclosure_AddAnimal_When_Full_Should_Throw_Exception()
    {
        // Arrange
        var enclosure = new Enclosure("Для хищников", "Маленький", 1);
        enclosure.AddAnimal(); // Заполняем вольер

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal());
    }

    // Тест 5. Проверка отметки кормления
    [Fact]
    public void FeedingSchedule_MarkAsCompleted_Should_Set_IsCompleted_To_True()
    {
        // Arrange
        var feedingSchedule = new FeedingSchedule(Guid.NewGuid(), DateTime.Now, "Мясо");

        // Act
        feedingSchedule.MarkAsCompleted();

        // Assert
        feedingSchedule.IsCompleted.Should().BeTrue();
    }

    // Тест 6. Проверка изменения времени кормления
    [Fact]
    public void FeedingSchedule_Reschedule_Should_Change_FeedingTime()
    {
        // Arrange
        var originalTime = DateTime.Now;
        var newTime = originalTime.AddHours(2);
        var feedingSchedule = new FeedingSchedule(Guid.NewGuid(), originalTime, "Мясо");

        // Act
        feedingSchedule.Reschedule(newTime);

        // Assert
        feedingSchedule.FeedingTime.Should().Be(newTime);
    }

    // Тест 7. Тест сервиса перемещения
    [Fact]
    public async Task AnimalTransferService_TransferAnimal_Should_Move_Animal_Successfully()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var fromEnclosureId = Guid.NewGuid();
        var toEnclosureId = Guid.NewGuid();

        var animal = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");

        var fromEnclosure = new Enclosure("Для хищников", "Большой", 3);
        var toEnclosure = new Enclosure("Для хищников", "Большой", 3);

        var animalRepositoryMock = new Mock<IAnimalRepository>();
        var enclosureRepositoryMock = new Mock<IEnclosureRepository>();

        animalRepositoryMock.Setup(repo => repo.GetByIdAsync(animalId))
            .ReturnsAsync(animal);

        enclosureRepositoryMock.Setup(repo => repo.GetByIdAsync(fromEnclosureId))
            .ReturnsAsync(fromEnclosure);

        enclosureRepositoryMock.Setup(repo => repo.GetByIdAsync(toEnclosureId))
            .ReturnsAsync(toEnclosure);

        var service = new AnimalTransferService(animalRepositoryMock.Object, enclosureRepositoryMock.Object);

        // Act
        await service.TransferAnimal(animalId, toEnclosureId);

        // Assert
        fromEnclosure.CurrentCapacity.Should().Be(0);
        toEnclosure.CurrentCapacity.Should().Be(1);
        animal.DomainEvents.Should().ContainSingle(e => e is AnimalMovedEvent);
    }

    // Тест 8. Тест ошибки перемещения
    [Fact]
    public async Task AnimalTransferService_TransferAnimal_When_TargetEnclosure_Full_Should_Throw_Exception()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var toEnclosureId = Guid.NewGuid();

        var animal = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");

        var toEnclosure = new Enclosure("Для хищников", "Маленький", 1);
        toEnclosure.AddAnimal(); // Делаем вольер полным

        var animalRepositoryMock = new Mock<IAnimalRepository>();
        var enclosureRepositoryMock = new Mock<IEnclosureRepository>();

        animalRepositoryMock.Setup(repo => repo.GetByIdAsync(animalId))
            .ReturnsAsync(animal);

        enclosureRepositoryMock.Setup(repo => repo.GetByIdAsync(toEnclosureId))
            .ReturnsAsync(toEnclosure);

        var service = new AnimalTransferService(animalRepositoryMock.Object, enclosureRepositoryMock.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.TransferAnimal(animalId, toEnclosureId));
    }

    // Тест 9. Тест создания расписания
    [Fact]
    public async Task FeedingOrganizationService_ScheduleFeeding_Should_Create_FeedingSchedule()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var feedingTime = DateTime.Now.AddHours(2);
        var foodType = "Мясо";

        var animal = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");

        var animalRepositoryMock = new Mock<IAnimalRepository>();
        var feedingScheduleRepositoryMock = new Mock<IFeedingScheduleRepository>();

        animalRepositoryMock.Setup(repo => repo.GetByIdAsync(animalId))
            .ReturnsAsync(animal);

        feedingScheduleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<FeedingSchedule>()))
            .Returns(Task.CompletedTask);

        var service = new FeedingOrganizationService(feedingScheduleRepositoryMock.Object, animalRepositoryMock.Object);

        // Act
        var result = await service.ScheduleFeedingAsync(animalId, feedingTime, foodType);

        // Assert
        result.Should().NotBeNull();
        result.AnimalId.Should().Be(animalId);
        result.FeedingTime.Should().Be(feedingTime);
        result.FoodType.Should().Be(foodType);
    }

    public bool IsHealthy1 => IsHealthy;

    public int GetCurrentCapacity()
    {
        return CurrentCapacity;
    }

    public bool GetIsHealthy()
    {
        return IsHealthy;
    }

    // Тест 10. Тест статистики
    [Fact]
    public async Task ZooStatisticsService_GetStatistics_Should_Return_Correct_Statistics()
    {
        // Arrange
        var animal1 = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");
        var animal2 = new Animal("Тигр", "Шерхан", new DateTime(2019, 5, 20), "Самец", "Мясо");

        // Устанавливаем статус здоровья для тестирования
        animal1.SetHealthStatusForTesting(true);   // Здоровое животное
        animal2.SetHealthStatusForTesting(false);  // Больное животное

        var animals = new List<Animal> { animal1, animal2 };

        var enclosures = new List<Enclosure>
    {
        new Enclosure("Для хищников", "Большой", 3),
        new Enclosure("Для травоядных", "Средний", 5),
        new Enclosure("Для птиц", "Маленький", 10)
    };

        // Устанавливаем текущую вместимость для тестирования
        enclosures[0].SetCurrentCapacityForTesting(2); // Первый вольер: 2 животных
        enclosures[1].SetCurrentCapacityForTesting(1); // Второй вольер: 1 животное  
        enclosures[2].SetCurrentCapacityForTesting(0); // Третий вольер: пустой

        var animalRepositoryMock = new Mock<IAnimalRepository>();
        var enclosureRepositoryMock = new Mock<IEnclosureRepository>();

        animalRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(animals);

        enclosureRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(enclosures);

        var service = new ZooStatisticsService(animalRepositoryMock.Object, enclosureRepositoryMock.Object);

        // Act
        var statistics = await service.GetStatistics();

        // Assert
        statistics.TotalAnimals.Should().Be(2);
        statistics.TotalEnclosures.Should().Be(3);
        statistics.FreeEnclosures.Should().Be(1); // Только один пустой вольер
        statistics.OccupiedEnclosures.Should().Be(2); // Два занятых вольера
        statistics.FullEnclosures.Should().Be(0); // Нет полностью заполненных вольеров
        statistics.SickAnimals.Should().Be(1);    // Одно больное животное
        statistics.HealthyAnimals.Should().Be(1); // Одно здоровое животное
    }

    // Тест 11. Тест сравнения сущностей
    [Fact]
    public void EntityBase_Equals_Should_Work_Correctly()
    {
        // Arrange
        var animal1 = new Animal("Лев", "Симба", new DateTime(2020, 1, 15), "Самец", "Мясо");
        var animal2 = new Animal("Тигр", "Шерхан", new DateTime(2019, 5, 20), "Самец", "Мясо");

        // Act & Assert
        animal1.Equals(animal1).Should().BeTrue();
        animal1.Equals(animal2).Should().BeFalse();
        animal1.Equals(null).Should().BeFalse();
    }

    // Тест 12. Тест свойств событий
    [Fact]
    public void DomainEvent_Should_Have_Correct_Properties()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var beforeCreation = DateTime.UtcNow;

        // Act
        var domainEvent = new AnimalFedEvent(animalId);
        var afterCreation = DateTime.UtcNow;

        // Assert
        domainEvent.Should().NotBeNull();
        domainEvent.EventId.Should().NotBeEmpty();
        domainEvent.OccurredOn.Should().BeOnOrAfter(beforeCreation);
        domainEvent.OccurredOn.Should().BeOnOrBefore(afterCreation);
        domainEvent.AnimalId.Should().Be(animalId);
    }
}