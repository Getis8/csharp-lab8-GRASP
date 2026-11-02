namespace Lab_8;

public class SingleTariffResolver : ITariffPolicyResolver
{
    private readonly ITariffPolicy _policy = new StandardTariffPolicy();

    public ITariffPolicy Resolve(DeliveryType type) => _policy;
}