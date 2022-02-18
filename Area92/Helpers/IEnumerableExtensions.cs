using System.Dynamic;
using System.Reflection;

namespace Area92.Helpers;

public static class IEnumerableExtensions
{
    public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> sources, string fields)
    {
        if (sources == null)
        {
            throw new ArgumentNullException(nameof(sources));
        }

        var response = new List<ExpandoObject>();

        // will keep the fields of TSource i.e. Anime(Entity)
        var propertyInfos = new List<PropertyInfo>();

        // no query param
        if (string.IsNullOrWhiteSpace(fields))
        {
            // Get all
            propertyInfos.AddRange(typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance));
        }
        else
        {
            var fieldsAfterSplit = fields.Split(","); // id title from request id,title
            // propertyInfos.AddRange(fieldsAfterSplit.Select(f => f.Trim()).Select(trimF => typeof(TSource).GetProperty(trimF, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)));

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                
                // Get the propertyInfo from propertyName
                var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase |
                    BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found on {typeof(TSource)}");
                }
                propertyInfos.Add(propertyInfo);
            }
        }

        foreach (TSource source in sources)
        {
            var expandoObject = new ExpandoObject();
            foreach (var propertyInfo in propertyInfos)
            {
                var value = propertyInfo.GetValue(source);
                ((IDictionary<string, object>)expandoObject).Add(propertyInfo.Name, value);
            }

            response.Add(expandoObject);
        }

        return response;
    }
}