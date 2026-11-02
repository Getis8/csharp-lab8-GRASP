namespace Lab_8;

public sealed class Shipment
{
    public int TrackingId { get; }
    public Sender Sender { get; }
    public Cargo Cargo { get; }
    public decimal TransportFee { get; }
    public decimal InsuranceFee { get; }
    public string Status { get; private set; }

    // Information Expert: загальна вартість завжди випливає зі складових
    public decimal TotalCost => TransportFee + InsuranceFee;

    // Закладені формули ведуть до єдиного джерела: політики і Cargo
    private Shipment(int trackingId, Sender sender, Cargo cargo, decimal transportFee, decimal insuranceFee)
    {
        TrackingId = trackingId;
        Sender = sender;
        Cargo = cargo;
        TransportFee = transportFee;
        InsuranceFee = insuranceFee;
        Status = "Created";
    }

    // Creator: єдина точка створення, об'єкт одразу валідний
    public static Shipment Create(int trackingId, Sender sender, Cargo cargo, ITariffPolicy tariffPolicy)
    {
        if (trackingId <= 0)
            throw new ArgumentException("Трекінг-ідентифікатор має бути додатним.", nameof(trackingId));
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(cargo);
        ArgumentNullException.ThrowIfNull(tariffPolicy);

        decimal transport = tariffPolicy.CalculateTransportFee(cargo);
        decimal insurance = cargo.CalculateInsurance();
        return new Shipment(trackingId, sender, cargo, transport, insurance);
    }

    public void Dispatch()
    {
        if (Status != "Created")
            throw new InvalidOperationException($"Відправку не можна відправити зі статусу '{Status}'.");
        Status = "Dispatched";
    }

    // Закон Деметри: клієнти питають Shipment, а не ходять через ланцюжок
    public string SenderCompanyName => Sender.CompanyName;
    public string SenderEmail => Sender.Email;
    public DeliveryType DeliveryType => Cargo.DeliveryType;
}