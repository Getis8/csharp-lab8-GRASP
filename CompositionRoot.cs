namespace Lab_8;

public static class CompositionRoot
{
    public static ShipmentConsoleUi CreateUi() =>
        new(
            new ShipmentController(
                new InMemoryShipmentRepository(),
                new ConsoleTrackingNotifier(),
                CreateTariffResolver()),
            new ConsoleCommandParser(),
            new WaybillFormatter());

    public static ITariffPolicyResolver CreateTariffResolver() =>
        new DictionaryTariffResolver(new ITariffPolicy[]
        {
            new StandardTariffPolicy(),
            new ExpressTariffPolicy(),
            new FreightTariffPolicy()
        });
}
