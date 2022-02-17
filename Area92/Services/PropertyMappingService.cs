using System;

namespace Area92.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        // Mapping from Request(AnimeForCreation) to Anime entity
        // "endYear" => "IsEnded"
        private Dictionary<string, PropertyMappingValue> _authorPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" }) },
                { "EndYear", new PropertyMappingValue(new List<string>() { "IsEnded" }) },
                { "IMDBRating", new PropertyMappingValue(new List<string>() { "IMDBRating" }) },
                { "ReleaseYear", new PropertyMappingValue(new List<string>() { "ReleaseYear"})},
                { "NumberOfSeasons", new PropertyMappingValue(new List<string>() { "NumberOfSeasons"})},
            };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<Models.AnimeForCreation, Entities.Anime>(_authorPropertyMapping));
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            // the string is separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            // run through the fields clauses
            foreach (var field in fieldsAfterSplit)
            {
                // trim
                var trimmedField = field.Trim();

                // remove everything after the first " " - if the fields 
                // are coming from an orderBy string, this part must be 
                // ignored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                // find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }

            return true;
        }


        public Dictionary<string, PropertyMappingValue> GetPropertyMapping
            <TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                                $"for <{typeof(TSource)},{typeof(TDestination)}");
        }
    }
}