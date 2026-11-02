namespace Lab_8;

public record ShipmentResult(
    int TrackingId,
    string CompanyName,
    string DeliveryType,
    decimal TransportFee,
    decimal InsuranceFee,
    decimal TotalCost,
    string Status);