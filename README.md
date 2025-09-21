# Финальный проект в рамках обучения по программе профессиональной переподготовки "C#-разработчик" в МФТИ

### Веб-приложение для автоматизации бизнес-процессов зоопарка с использованием Domain-Driven Design и Clean Architecture.

# Описание задания

### Вам предстоит разработать веб-приложение для автоматизации следующих бизнес-процессов зоопарка: управление животными; вольерами; расписанием кормлений.

В ходе общения с заказчиком были выявлены следующие требования к функциональности проектируемого модуля веб-приложения

`Use Cases`:
- Добавить / удалить животное
- Добавить / удалить вольер
- Переместить животное между вольерами
- Просмотреть расписание кормления
- Добавить новое кормление в расписание
- Просмотреть статистику зоопарка (кол-во животных, свободные вольеры и т.д.).

После встречи с доменными экспертами. 

Вы определили три основных класса: `Animal`, `Enclosure` и `FeedingSchedule`. Архитектор проекта предложил вам следующий вариант создания богатой модели предметной области:

Животное (`Animal`)
- Вид, кличка, дата рождения, пол, любимая еда, статус (здоров/болен)
- Методы: кормить, лечить, переместить в другой вольер

Вольер (`Enclosure`)
- Тип (для хищников, травоядных, птиц, аквариум и т.д.), размер, текущее количество животных, максимальная вместимость
- Методы: добавить животное, убрать животное, провести уборку

Расписание кормления (`FeedingSchedule`)
- Животное, время кормления, тип пищи
- Методы: изменить расписание, отметить выполнение

Выявление `Value Object` оставили на ваше усмотрение.

На kickoff митинге с командой было приняли решение, в первую очередь, реализовать следующие сервисы:
- `AnimalTransferService` (для перемещения животных между вольерами)
- `FeedingOrganizationService` (для организации процесса кормления)
- `ZooStatisticsService` (для сбора статистики по зоопарку)

Определить и реализовать следующие доменные события:
- `AnimalMovedEvent` (при перемещении животного)
- `FeedingTimeEvent` (при наступлении времени кормления)

Определили следующую структуру проекта, согласно **Clean Architecture**:
- `Domain` (ядро, содержит наши модели)
- `Application` (содержит сервисы, реализующие бизнес-логику приложения)
- `Infrastructure` (внешние взаимодействия)
- `Presentation` (контроллеры нашего веб-приложения)

ТРЕБУЕТСЯ

1. Разработать классы доменной модели согласно концепции **Domain-Driven Design**
2. Построить структуру проекта соблюдая принципы **Clean Architecture**.
3. В слое представления (`Web API` в архитектурном стиле `REST API`) реализовать следующие контроллеры:
- Контроллер для работы с животными (просмотр информации о животных, добавление новых, удаление);
- Контроллер для работы с вольерами;
4. При помощи инструмента проектирования API (например `Swagger`) произвести тестирование нашего приложения:
- Добавить новые сущности (вольер, животное, расписание кормления);
- Получить информации о животных, вольерах и расписание кормления;
- Выполнить операции кормления, перемещения и т.д.
5. Хранение данных организовать в виде in-memory хранилища (`Infrastructure Layer`)
6. Написать отчет, в котором отразить:
- какие пункты из требуемого функционала вы реализовали и в каких классах \ модулях их можно увидеть. 
- какие концепции **Domain-Driven Design** и принципы **Clean Architecture** вы применили, скажите в каких классах (модулях).

Критерии оценки

1. Реализация основных требований к функциональности
2. Соблюдение принципов **Clean Architecture**:
- Слои должны зависеть только внутрь (`Domain` не зависит ни от чего);
- Все зависимости между слоями через интерфейсы;
- Бизнес-логика полностью изолирована в `Domain` и `Application` слоях.
3. Соблюдены концепции **Domain-Driven Design**:
- Использование `Value Objects` для примитивов;
- Инкапсуляция бизнес-правил внутри доменных объектов.
4. Покрыто тестами более 65% кода.

# Реализация проектного решения

## Архитектура

- **Domain Layer**: Бизнес-логика и доменные модели
- **Application Layer**: Сервисы приложения
- **Infrastructure Layer**: Репозитории и данные
- **API Layer**: Web API контроллеры

## Функциональность

- Управление животными (добавление, удаление, перемещение)
- Управление вольерами
- Расписание кормлений
- Статистика зоопарка

## Технологии

- .NET 8.0
- ASP.NET Core Web API
- Swagger
- Clean Architecture
- Domain-Driven Design

## Установка и запуск через Visual Studio

1. Клонируйте репозиторий
2. Откройте `ZooManagement.sln`
3. Установите `API` как Startup Project
4. Нажмите F5 или Ctrl + F5
5. Перейдите: `https://localhost:7201/swagger`


## xUnit-Тестирование через Visual Studio

1. Откройте Test Explorer (Test → Test Explorer)
2. Нажмите "Run All Tests"

## Тестирование приложения через API Endpoints

Используйте Swagger UI для тестирования API endpoints после запуска:
 
- Swagger UI: `https://localhost:7201/swagger`
- Animals API: `https://localhost:7201/api/Animals`
- Enclosures API: `https://localhost:7201/api/Enclosures`
- Statistics API: `https://localhost:7201/api/Statistics`

## Отчет по проекту!

### 1. Реализованный функционал

Use Cases из задания:

a. Добавить / удалить животное:
- Контроллер: `AnimalsController` (POST /api/Animals, DELETE /api/Animals/{id})
- Сервис: `InMemoryAnimalRepository` (AddAsync, DeleteAsync)
- Модель: `Animal` entity

b. Добавить / удалить вольер
- Контроллер: `EnclosuresController` (POST /api/Enclosures, DELETE /api/Enclosures/{id})
- Сервис: `InMemoryEnclosureRepository` (AddAsync, DeleteAsync)
- Модель: `Enclosure` entity

c. Переместить животное между вольерами
- Контроллер: `AnimalsController` (POST /api/Animals/{id}/transfer/{enclosureId})
- Сервис: `AnimalTransferService` (TransferAnimal)
- Событие: `AnimalMovedEvent`

d. Просмотреть расписание кормления
- Контроллер: `FeedingSchedulesController` (GET /api/FeedingSchedules)
- Сервис: `InMemoryFeedingScheduleRepository` (GetAllAsync, GetByAnimalIdAsync)
- Модель: `FeedingSchedule` entity

e. Добавить новое кормление в расписание
- Контроллер: `FeedingSchedulesController` (POST /api/FeedingSchedules)
- Сервис: `FeedingOrganizationService` (ScheduleFeedingAsync)
- Модель: `FeedingSchedule` entity

f. Просмотреть статистику зоопарка
- Контроллер: `StatisticsController` (GET /api/Statistics/zoo, GET /api/Statistics/feeding)
- Сервис: `ZooStatisticsService` (GetStatistics)
- Сервис: `FeedingOrganizationService` (GetFeedingStatisticsAsync)

Дополнительный реализованный функционал:
- Отметка кормления как выполненного: `FeedingSchedulesController` (POST /api/FeedingSchedules/{id}/complete)
- Уборка вольеров: `EnclosuresController` (POST /api/Enclosures/{id}/clean)
- Просмотр предстоящих кормлений: `FeedingSchedulesController` (GET /api/FeedingSchedules/upcoming)
- Получение животных по ID: `AnimalsController` (GET /api/Animals/{id})
- Получение вольеров по ID: `EnclosuresController` (GET /api/Enclosures/{id})

Разработаны и успешно пройдены 12 Unit-тестов (Tests) примерно 70% покрытия кода

Описание тестов:
1. `Animal_Feed_Should_Raise_AnimalFedEvent` - проверяет генерацию события кормления
2. `Animal_Heal_Should_Set_IsHealthy_To_True` - проверяет лечение животного
3. `Enclosure_AddAnimal_Should_Increase_CurrentCapacity` - проверяет добавление животного в вольер
4. `Enclosure_AddAnimal_When_Full_Should_Throw_Exception` - проверяет обработку переполнения вольера
5. `FeedingSchedule_MarkAsCompleted_Should_Set_IsCompleted_To_True` - проверяет отметку кормления
6. `FeedingSchedule_Reschedule_Should_Change_FeedingTime` - проверяет изменение времени кормления
7. `AnimalTransferService_TransferAnimal_Should_Move_Animal_Successfully` - тестирует сервис перемещения
8. `AnimalTransferService_TransferAnimal_When_TargetEnclosure_Full_Should_Throw_Exception` - тестирует ошибки перемещения
9. `FeedingOrganizationService_ScheduleFeeding_Should_Create_FeedingSchedule` - тестирует создание расписания
10. `ZooStatisticsService_GetStatistics_Should_Return_Correct_Statistics` - тестирует статистику
11. `EntityBase_Equals_Should_Work_Correctly` - тестирует сравнение сущностей
12. `DomainEvent_Should_Have_Correct_Properties` - тестирует свойства событий

### 2. Примененные концепции **Domain-Driven Design** и **Clean Architecture**

### Clean Architecture:

1. Domain Layer (`Domain`)
- Сущности: `Animal`, `Enclosure`, `FeedingSchedule`, `EntityBase`
- События: `DomainEvent`, `AnimalMovedEvent`, `FeedingTimeEvent`, `AnimalFedEvent`, `FeedingCompletedEvent`
- Интерфейсы: `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`
- Принцип: Ядро системы не зависит от внешних слоев

2. Application Layer (`Application`)
- Сервисы: `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`
- DTO: `CreateAnimalRequest`, `CreateEnclosureRequest`, `CreateFeedingScheduleRequest`, `AnimalResponse`, `EnclosureResponse`
- Принцип: Содержит бизнес-логику приложения, зависит только от `Domain` Layer

3. Infrastructure Layer (`Infrastructure`)
Реализации репозиториев: `InMemoryAnimalRepository`, `InMemoryEnclosureRepository`, `InMemoryFeedingScheduleRepository`
Принцип: Реализация внешних зависимостей, зависит от `Domain` и `Application` Layers

4. Presentation Layer (`API`)
- Контроллеры: `AnimalsController`, `EnclosuresController`, `FeedingSchedulesController`, `StatisticsController`
- Принцип: Внешний интерфейс системы, зависит от `Application` Layer

### Domain-Driven Design:

1. Rich Domain Model
- Классы: `Animal`, `Enclosure`, `FeedingSchedule`
  Поведение в сущностях:
- `Animal.Feed()`, `Animal.Heal()`, `Animal.MoveToEnclosure()`
- `Enclosure.AddAnimal()`, `Enclosure.RemoveAnimal()`, `Enclosure.Clean()`
- `FeedingSchedule.MarkAsCompleted()`, `FeedingSchedule.Reschedule()`

2. Aggregate Roots
- Сущности: `Animal`, `Enclosure`, `FeedingSchedule` как корни агрегатов
- Инварианты: `Enclosure.CanAddAnimal()` проверяет вместимость

3. Domain Events
- События: `AnimalMovedEvent`, `FeedingTimeEvent`, `AnimalFedEvent`, `FeedingCompletedEvent`
- Генерация: Вызываются через `AddDomainEvent()` в методах доменных сущностей

4. Repositories Pattern
- Интерфейсы: `IAnimalRepository`, `IEnclosureRepository`, `IFeedingScheduleRepository`
- Реализации: InMemory репозитории в `Infrastructure` Layer
- Принцип: Разделение ответственности за доступ к данным

5. Value Objects
- Реализация: DTO классы в Application Layer (`AnimalResponse`, `EnclosureResponse`)
- Назначение: Передача данных между слоями без экспозиции доменных сущностей

6. Bounded Contexts
- Контекст управления животными: `Animal` entity + `AnimalTransferService`
- Контекст управления вольерами: `Enclosure` entity
- Контекст кормления: `FeedingSchedule` entity + `FeedingOrganizationService`
- Контекст статистики: `ZooStatisticsService`

### Принципы SOLID и Clean Code:

Single Responsibility Principle
- Каждый сервис и контроллер имеет одну ответственность
- Репозитории отвечают только за доступ к данным

Dependency Inversion Principle
- Интерфейсы репозиториев в Domain Layer
- Реализации в Infrastructure Layer
- Dependency Injection через конструкторы

Open/Closed Principle
- Легко добавить новые репозитории (например, для БД)
- Возможность расширения функционала через новые сервисы

Interface Segregation Principle
- Каждый репозиторий имеет свой специализированный интерфейс

Тестирование:
Unit Tests (`Tests`)
- Тесты доменных сущностей: `AnimalTests`, `EnclosureTests`, `FeedingScheduleTests`
- Тесты сервисов: `AnimalTransferServiceTests`, `FeedingOrganizationServiceTests`, `ZooStatisticsServiceTests`

### 3. Технологический стек
- `.NET 8.0` - основная платформа
- `ASP.NET Core Web API` - веб-фреймворк
- `Swagger` - документация и тестирование API
- `xUnit` - фреймворк для unit-тестирования
- `FluentAssertions` - библиотека для assertions
- `In-Memory Storage` - временное хранилище данных

### 4. Архитектурные решения

In-Memory репозитории
- Быстрая реализация для демонстрации
- Легко заменить на реальную БД

Separation of Concerns
- Четкое разделение между слоями
- Легкая поддержка и тестирование

Event-Driven Architecture
- Доменные события для отслеживания изменений
- Возможность добавления обработчиков событий

---
## Вывод: Проект успешно реализует все требуемые функции с соблюдением принципов Clean Architecture и Domain-Driven Design.
