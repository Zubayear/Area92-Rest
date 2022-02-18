using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Area92.Filters
{
    public class AnimesResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 ||
                resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            // IMappervar expandoObjects = resultFromAction.Value as IEnumerable<IDictionary<string, object>>;
            // foreach (var expandoObject in expandoObjects)
            // {
            //     mapper.Map<IEnumerable<Models.Anime>>(expandoObject["value"]);
            // }
            // var shapedAnimesWithLinks = shapedAnimes.Select(anime =>
            // {
            //     var animeDictionary = anime as IDictionary<string, object>;
            //     animeDictionary.Add("links", GenerateLinksForAnime((Guid)animeDictionary["Id"], null));
            //     return animeDictionary;
            // });
            // var responseDictionary = new Dictionary<string, object>
            // {
            //     { "value", shapedAnimesWithLinks },
            //     { "links", links }
            // };
            resultFromAction.Value = mapper.Map<IEnumerable<Models.Anime>>(resultFromAction.Value);
            // resultFromAction.Value = expandoObject;
            await next();
        }
    }
}