namespace Lab_8;

public class WaybillFormatter
{
    public string Format(ShipmentResult r) =>
        $"Товарно-транспортна накладна TTN-{r.TrackingId}\n" +
        $"Відправник: {r.CompanyName}\n" +
        $"Тип: {r.DeliveryType}\n" +
        $"Транспортування: {r.TransportFee:F2} грн\n" +
        $"Страховка: {r.InsuranceFee:F2} грн\n" +
        $"Загальна вартість: {r.TotalCost:F2} грн";
}