using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Area92.ModelBinders
{
    public class ArrayModelBinder : IModelBinder
    {
        private readonly ILogger<ArrayModelBinder> logger;

        public ArrayModelBinder(ILogger<ArrayModelBinder> logger)
        {
            this.logger = logger;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
            logger.LogInformation($"Value from bindingContext: {value}");

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            var elementType = bindingContext.ModelType.GetTypeInfo()
                .GenericTypeArguments[0];
            logger.LogInformation($"Value from bindingContext.ModelType: {elementType}");

            // Get converter of type System.Guid
            var converter = TypeDescriptor.GetConverter(elementType);

            // 2fbd9936-736d-440c-d2f8-08d9f138ebeb,319ce650-e4b7-46ed-d2f9-08d9f138ebeb
            // convert this to actual guid array
            var values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            // create 2 lenth array of type System.Guid
            var typedValues = Array.CreateInstance(elementType, values.Length);

            // copy values from 0th index of System.Guid array
            values.CopyTo(typedValues, 0);

            bindingContext.Model = typedValues;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
