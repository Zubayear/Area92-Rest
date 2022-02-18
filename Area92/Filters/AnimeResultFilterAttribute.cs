using Area92.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Area92.Filters
{
    public class AnimeResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }
            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            // storing the links array
            var expandoObjectWithLinks = resultFromAction.Value;
            resultFromAction.Value = mapper.Map<Models.Anime>(resultFromAction.Value);
            // adding after the mapping
            resultFromAction.Value = expandoObjectWithLinks;
            await next();
        }
    }
}
