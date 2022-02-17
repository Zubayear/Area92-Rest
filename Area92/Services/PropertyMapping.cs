namespace Area92.Services;

public class PropertyMapping<TSource, TDestination> : IPropertyMapping
{
    public Dictionary<string, PropertyMappingValue> mappingDictionary { get; private set; }

    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        this.mappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
    }
}