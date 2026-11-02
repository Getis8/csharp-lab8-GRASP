namespace Lab_8;

public class InMemoryShipmentRepository : IShipmentRepository
{
    private readonly List<Shipment> _shipments = new();
    private int _lastId = 770098;

    public int NextTrackingId() => ++_lastId;

    public void Save(Shipment shipment)
    {
        ArgumentNullException.ThrowIfNull(shipment);
        _shipments.Add(shipment);
    }

    public IReadOnlyList<Shipment> GetAll() => _shipments.AsReadOnly();
}