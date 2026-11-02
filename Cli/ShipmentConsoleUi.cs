namespace Lab_8;

public class ShipmentConsoleUi
{
    private readonly ShipmentController _controller;
    private readonly ConsoleCommandParser _parser;
    private readonly WaybillFormatter _formatter;

    public ShipmentConsoleUi(ShipmentController controller, ConsoleCommandParser parser, WaybillFormatter formatter)
    {
        _controller = controller;
        _parser = parser;
        _formatter = formatter;
    }

    public void Run()
    {
        Console.WriteLine("=== Оформлення відправки вантажу ===");
        Console.Write("Введіть назву компанії-відправника: ");
        string company = Console.ReadLine() ?? string.Empty;
        Console.Write("Введіть тип доставки (Standard/Express/Freight): ");
        string type = Console.ReadLine() ?? string.Empty;

        try
        {
            var command = _parser.Parse(company, type);
            var result = _controller.CreateShipment(command);
            Console.WriteLine(_formatter.Format(result));
            Console.WriteLine($"[Реєстр] Накладну TTN-{result.TrackingId} додано у базу відправок.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Помилка введення: {ex.Message}");
        }
    }
}