namespace Lab_8;

// Нова політика додається без зміни Shipment, Cargo чи контролера
public sealed class SameDayTariffPolicy : ITariffPolicy
{
    public DeliveryType Type => (DeliveryType)100;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * StandardTariffPolicy.BaseRatePerKg * 2.5m;
}

public static class PolymorphismDemo
{
    public static void Run()
    {
        var cargoStandard = Cargo.Create(120.0, 1.5, 50000m, DeliveryType.Standard);
        var cargoExpress = Cargo.Create(120.0, 1.5, 50000m, DeliveryType.Express);
        var cargoFreight = Cargo.Create(120.0, 1.5, 50000m, DeliveryType.Freight);

        var resolver = CompositionRoot.CreateTariffResolver();

        foreach (var cargo in new[] { cargoStandard, cargoExpress, cargoFreight })
        {
            var fee = resolver.Resolve(cargo.DeliveryType).CalculateTransportFee(cargo);
            Console.WriteLine($"{cargo.DeliveryType,-9} -> транспортування: {fee:F2} грн");
        }

        Console.WriteLine("\nНова політика SameDay підключається без зміни наявного коду:");
        var sameDay = new SameDayTariffPolicy();
        Console.WriteLine($"SameDay   -> {sameDay.CalculateTransportFee(cargoStandard):F2} грн");
    }
}