# ADR-003: Тонкий CLI, Use-Case Controller та репозиторій як Pure Fabrication

## Статус
Прийнято (Accepted)

## Контекст
Один метод legacy-коду вів діалог з користувачем, рахував, форматував
накладну, «зберігав» і «надсилав» email (Bloated Controller).

## Рішення
- **Controller:** `ShipmentController` приймає DTO-команду, створює
  `Shipment`, зберігає через репозиторій, не містить `Console`.
- **Indirection:** контролер залежить від `IShipmentRepository` та
  `ITrackingNotifier`, а не від конкретних класів.
- **Pure Fabrication:** `ConsoleCommandParser`, `WaybillFormatter`,
  `InMemoryShipmentRepository` не належать до бізнес-домену, але
  тримають сутності чистими.

## Наслідки
* **Позитивні:** CLI без бізнес-логіки, контролер без введення-виведення,
  легко підмінити сховище та email.
* **Негативні:** більше класів і абстракцій.