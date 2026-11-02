namespace Lab_8;

public class ShipmentController
{
    private readonly IShipmentRepository _repository;
    private readonly ITrackingNotifier _notifier;
    private readonly ITariffPolicyResolver _tariffResolver;

    public ShipmentController(
        IShipmentRepository repository,
        ITrackingNotifier notifier,
        ITariffPolicyResolver tariffResolver)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        _tariffResolver = tariffResolver ?? throw new ArgumentNullException(nameof(tariffResolver));
    }

    public ShipmentResult CreateShipment(CreateShipmentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!Enum.TryParse<DeliveryType>(command.DeliveryType, ignoreCase: true, out var deliveryType))
            throw new ArgumentException($"Невідомий тип доставки: {command.DeliveryType}");

        var sender = Sender.Create(55, command.CompanyName, "Іван Петренко", "logistics@company.ua");
        var cargo = Cargo.Create(command.WeightKg, command.VolumeCubMetres, command.DeclaredValue, deliveryType);

        var policy = _tariffResolver.Resolve(deliveryType);
        var shipment = Shipment.Create(_repository.NextTrackingId(), sender, cargo, policy);

        shipment.Dispatch();
        _repository.Save(shipment);
        _notifier.SendTrackingCode(shipment.SenderEmail, shipment.TrackingId);

        return new ShipmentResult(
            shipment.TrackingId,
            shipment.SenderCompanyName,
            shipment.DeliveryType.ToString(),
            shipment.TransportFee,
            shipment.InsuranceFee,
            shipment.TotalCost,
            shipment.Status);
    }
}