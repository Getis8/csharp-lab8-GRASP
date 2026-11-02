using Lab_8;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    Console.WriteLine("\n=== Лабораторна робота №8 (варіант 8, GRASP) ===");
    Console.WriteLine("0 - Legacy-код (до рефакторингу)");
    Console.WriteLine("1 - Оформити відправку (після рефакторингу)");
    Console.WriteLine("2 - Демо поліморфізму тарифів");
    Console.WriteLine("Q - Вихід");
    Console.Write("Ваш вибір: ");

    switch (Console.ReadLine()?.Trim().ToUpperInvariant())
    {
        case "0": new Lab_8.Legacy.LegacyLogisticsProcessor().ProcessShipmentConsole(); break;
        case "1": CompositionRoot.CreateUi().Run(); break;
        case "Q": return;
        default: Console.WriteLine("Невірний вибір"); break;
    }
}