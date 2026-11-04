namespace Lab_8;

public sealed class ExpressTariffPolicy : ITariffPolicy
{
    public const decimal Multiplier = 1.8m;

    public DeliveryType Type => DeliveryType.Express;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * StandardTariffPolicy.BaseRatePerKg * Multiplier;
}

public sealed class FreightTariffPolicy : ITariffPolicy
{
    public const decimal Multiplier = 1.2m;

    public DeliveryType Type => DeliveryType.Freight;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * StandardTariffPolicy.BaseRatePerKg * Multiplier;
}