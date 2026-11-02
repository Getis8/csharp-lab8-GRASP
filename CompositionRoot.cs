namespace Lab_8;

public static class CompositionRoot
{
    public static ShipmentConsoleUi CreateUi() =>
        new(
            new ShipmentController(
                new InMemoryShipmentRepository(),
                new ConsoleTrackingNotifier(),
                new SingleTariffResolver()),
            new ConsoleCommandParser(),
            new WaybillFormatter());
}