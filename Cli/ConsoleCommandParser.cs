namespace Lab_8;

public class ConsoleCommandParser
{
    private const double DefaultWeightKg = 120.0;
    private const double DefaultVolume = 1.5;
    private const decimal DefaultDeclaredValue = 50000m;

    public CreateShipmentCommand Parse(string companyName, string deliveryType) =>
        new(companyName, deliveryType, DefaultWeightKg, DefaultVolume, DefaultDeclaredValue);
}