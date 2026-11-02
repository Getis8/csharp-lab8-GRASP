#nullable disable

namespace Lab_8.Legacy;

 // Опис предметної області: Логістична компанія та вантажоперевезення.
    // Завдання коду: Оформлення вантажної відправки — розрахунок вартості доставки залежно від ваги, об'єму та типу транспорту 
      // (Standard, Express, Freight), страхового відсотка вантажу, створення товарно-транспортної накладної, 
      // збереження у реєстр відправок та відправка трекінг-коду по email.
public class Sender
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
}

public class Cargo
{
    public double WeightKg { get; set; }
    public double VolumeCubMetres { get; set; }
    public decimal DeclaredValue { get; set; }
    public string DeliveryType { get; set; } // "Standard", "Express", "Freight"
}

public class Shipment
{
    public int TrackingId { get; set; }
    public Sender Sender { get; set; }
    public Cargo Cargo { get; set; }
    public decimal TransportFee { get; set; }
    public decimal InsuranceFee { get; set; }
    public decimal TotalCost { get; set; }
    public string Status { get; set; }
}

public class LegacyLogisticsProcessor
{
    public void ProcessShipmentConsole()
    {
        Console.WriteLine("=== Оформлення відправки вантажу ===");
        Console.Write("Введіть назву компанії-відправника: ");
        string company = Console.ReadLine();
        Console.Write("Введіть тип доставки (Standard/Express/Freight): ");
        string deliveryType = Console.ReadLine();

        Sender sender = new Sender { Id = 55, CompanyName = company, ContactPerson = "Іван Петренко", Email = "logistics@company.ua" };
        Cargo cargo = new Cargo { WeightKg = 120.0, VolumeCubMetres = 1.5, DeclaredValue = 50000m, DeliveryType = deliveryType };

        Shipment shipment = new Shipment
        {
            TrackingId = 770099,
            Sender = sender,
            Cargo = cargo,
            Status = "Created"
        };

        decimal baseRatePerKg = 15m;
        decimal baseTransport = (decimal)shipment.Cargo.WeightKg * baseRatePerKg;

        if (shipment.Cargo.DeliveryType == "Express")
        {
            baseTransport *= 1.8m;
        }
        else if (shipment.Cargo.DeliveryType == "Freight")
        {
            baseTransport *= 1.2m;
        }

        decimal insurance = shipment.Cargo.DeclaredValue * 0.005m; // 0.5% страховка

        shipment.TransportFee = baseTransport;
        shipment.InsuranceFee = insurance;
        shipment.TotalCost = shipment.TransportFee + shipment.InsuranceFee;
        shipment.Status = "Dispatched";

        string waybill = $"Товарно-транспортна накладна TTN-{shipment.TrackingId}\nВідправник: {shipment.Sender.CompanyName}\nТип: {shipment.Cargo.DeliveryType}\nТранспортування: {shipment.TransportFee} грн\nСтраховка: {shipment.InsuranceFee} грн\nЗагальна вартість: {shipment.TotalCost} грн";
        Console.WriteLine(waybill);

        Console.WriteLine($"[Реєстр] Накладну TTN-{shipment.TrackingId} додано у базу відправок.");
        Console.WriteLine($"[Email] Трекінг-код TTN-{shipment.TrackingId} надіслано на {shipment.Sender.Email}");
    }
}