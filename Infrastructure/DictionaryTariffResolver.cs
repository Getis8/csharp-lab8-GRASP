namespace Lab_8;

public class DictionaryTariffResolver : ITariffPolicyResolver
{
    private readonly Dictionary<DeliveryType, ITariffPolicy> _policies;

    public DictionaryTariffResolver(IEnumerable<ITariffPolicy> policies)
    {
        _policies = policies.ToDictionary(p => p.Type);
    }

    public ITariffPolicy Resolve(DeliveryType type)
    {
        if (!_policies.TryGetValue(type, out var policy))
            throw new ArgumentException($"Для типу доставки {type} не зареєстровано тарифну політику.");
        return policy;
    }
}