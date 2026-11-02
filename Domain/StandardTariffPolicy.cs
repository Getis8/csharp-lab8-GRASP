namespace Lab_8;

public sealed class StandardTariffPolicy : ITariffPolicy
{
    public const decimal BaseRatePerKg = 15m;

    public DeliveryType Type => DeliveryType.Standard;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * BaseRatePerKg;
}