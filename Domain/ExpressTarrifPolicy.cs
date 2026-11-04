namespace Lab_8;

public sealed class ExpressTariffPolicy : ITariffPolicy
{
    public const decimal BaseRatePerKg = 15m;
    public const decimal ExpressMultiplier = 1.8m;
    
    public DeliveryType Type => DeliveryType.Express;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * BaseRatePerKg * ExpressMultiplier;
}