namespace Lab_8;

public sealed class FreightTariffPolicy : ITariffPolicy
{
    public const decimal BaseRatePerKg = 15m;
    public const decimal FreightMultiplier = 1.2m;
    
    public DeliveryType Type => DeliveryType.Freight;

    public decimal CalculateTransportFee(Cargo cargo) =>
        (decimal)cargo.WeightKg * BaseRatePerKg * FreightMultiplier;
}