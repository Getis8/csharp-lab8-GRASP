namespace Lab_8;

public class TariffPolicyResolver : ITariffPolicyResolver
{
    private readonly Dictionary<DeliveryType, ITariffPolicy> _policies;

    public TariffPolicyResolver(IEnumerable<ITariffPolicy> policies)
    {
        _policies = policies.ToDictionary(p => p.Type);
    }

    public ITariffPolicy Resolve(DeliveryType type)
    {
        if (_policies.TryGetValue(type, out var policy))
            return policy;
            
        throw new NotSupportedException($"Тариф для {type} не підтримується.");
    }
}