namespace Lab_8;

public interface ITariffPolicy
{
    DeliveryType Type { get; }
    decimal CalculateTransportFee(Cargo cargo);
}