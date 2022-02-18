using System.Reflection;

namespace Area92.Services;

public class PropertyCheckerService : IPropertyCheckerService
{
    public bool PropertyExists<TSource>(string fields)
    {
        if (string.IsNullOrWhiteSpace(fields))
        {
            return true;
        }

        var fieldsAfterSplit = fields.Split(",");
        foreach (var field in fieldsAfterSplit)
        {
            var propertyName = field.Trim();
            var propertyInfo = typeof(TSource).GetProperty(propertyName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (propertyInfo == null)
            {
                return false;
            }
        }

        return true;
    }
}