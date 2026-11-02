namespace Lab_8;

public class ConsoleTrackingNotifier : ITrackingNotifier
{
    public void SendTrackingCode(string email, int trackingId) =>
        Console.WriteLine($"[Email] Трекінг-код TTN-{trackingId} надіслано на {email}");
}