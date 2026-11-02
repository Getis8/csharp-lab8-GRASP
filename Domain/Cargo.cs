namespace Lab_8;

public sealed class Cargo
{
    public const decimal InsuranceRate = 0.005m; // 0.5%, єдине джерело правди

    public double WeightKg { get; }
    public double VolumeCubMetres { get; }
    public decimal DeclaredValue { get; }
    public DeliveryType DeliveryType { get; }

    private Cargo(double weightKg, double volumeCubMetres, decimal declaredValue, DeliveryType deliveryType)
    {
        WeightKg = weightKg;
        VolumeCubMetres = volumeCubMetres;
        DeclaredValue = declaredValue;
        DeliveryType = deliveryType;
    }

    public static Cargo Create(double weightKg, double volumeCubMetres, decimal declaredValue, DeliveryType deliveryType)
    {
        if (weightKg <= 0)
            throw new ArgumentException("Вага має бути додатною.", nameof(weightKg));
        if (volumeCubMetres <= 0)
            throw new ArgumentException("Об'єм має бути додатним.", nameof(volumeCubMetres));
        if (declaredValue < 0)
            throw new ArgumentException("Заявлена вартість не може бути від'ємною.", nameof(declaredValue));
        if (!Enum.IsDefined(deliveryType))
            throw new ArgumentException("Невідомий тип доставки.", nameof(deliveryType));

        return new Cargo(weightKg, volumeCubMetres, declaredValue, deliveryType);
    }

    // Information Expert: цінність вантажу знає саме Cargo
    public decimal CalculateInsurance() => DeclaredValue * InsuranceRate;
}