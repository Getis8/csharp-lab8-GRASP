namespace Lab_8;

public record CreateShipmentCommand(
    string CompanyName,
    string DeliveryType,
    double WeightKg,
    double VolumeCubMetres,
    decimal DeclaredValue);