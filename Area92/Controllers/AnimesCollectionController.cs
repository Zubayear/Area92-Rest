using Area92.Filters;
using Area92.ModelBinders;
using Area92.Models;
using Area92.ResourceParameters;
using Area92.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Area92.Controllers
{
    [ApiController]
    [Route("api/animescollections")]
    [AnimesResultFilterAttribute]
    public class AnimesCollectionController : ControllerBase
    {
        private readonly IAnimeRepository animeRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public AnimesCollectionController(IAnimeRepository animeRepository,
            ILogger<AnimesCollectionController> logger, IMapper mapper)
        {
            this.animeRepository = animeRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        // api/animescollection/(id1,id2..)
        [HttpGet]
        [Route("({ids})", Name = "GetAnimesCollection")]
        //[AnimeResultFilterAttribute]
        public async Task<IActionResult> GetAnimesCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            logger.LogInformation($"Id: {ids}");
            var animes = await animeRepository.GetAllAnime(ids);
            if (animes.Count() != ids.Count())
            {
                return NotFound();
            }
            return Ok(animes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimesCollection(IEnumerable<AnimeForCreation> animesForCreation)
        {
            logger.LogInformation("Request: " + animesForCreation.ToString());
            // map it to entity that we'll save to db
            var entityToSave = mapper.Map<IEnumerable<Entities.Anime>>(animesForCreation);
            foreach (var entity in entityToSave)
            {
                animeRepository.Save(entity);
            }

            await animeRepository.SaveChanges();

            // Get animes from db by passing IEnumerable<Guid>
            var animesToReturn = await animeRepository.GetAllAnime(entityToSave.Select(anime => anime.Id).ToList());


            var ids = string.Join(",", animesToReturn.Select(anime => anime.Id));
            return CreatedAtRoute("GetAnimesCollection", new { ids }, animesToReturn);
        }
    }
}
