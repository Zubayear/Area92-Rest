namespace Area92.Services;

public interface IPropertyCheckerService
{
    bool PropertyExists<TSource>(string fields);
}