namespace Lab_8;

public interface IShipmentRepository
{
    void Save(Shipment shipment);
    int NextTrackingId();
}

public interface ITrackingNotifier
{
    void SendTrackingCode(string email, int trackingId);
}

public interface ITariffPolicyResolver
{
    ITariffPolicy Resolve(DeliveryType type);
}