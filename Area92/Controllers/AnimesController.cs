using System.Text.Json;
using Area92.Filters;
using Area92.Helpers;
using Area92.Models;
using Area92.ResourceParameters;
using Area92.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Anime = Area92.Entities.Anime;

namespace Area92.Controllers
{
    [ApiController]
    [Route("api/animes")]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeRepository animeRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IPropertyMappingService _propertyMappingService;

        public AnimesController(IAnimeRepository animeRepository,
            ILogger<AnimesController> logger, IMapper mapper, IPropertyMappingService propertyMappingService)
        {
            this.animeRepository = animeRepository ?? throw new ArgumentNullException(nameof(animeRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService =
                propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        [HttpGet(Name = "GetAllAnime")]
        [AnimesResultFilterAttribute]
        public async Task<IActionResult> GetAllAnime([FromQuery] AnimesResourceParameter animesResourceParameter)
        {
            logger.LogInformation("Get Request for All Anime: {AnimesResourceParameter}", animesResourceParameter);

            if (!_propertyMappingService
                    .ValidMappingExistsFor<AnimeForCreation, Anime>(animesResourceParameter.OrderBy))
            {
                return BadRequest();
            }

            var animesEntity = await animeRepository.GetAnimes(animesResourceParameter);
            var previousPageLink = animesEntity.HasPrevious
                ? CreateAnimesResourceUri(animesResourceParameter, ResourceUriType.PreviousPage)
                : null;
            var nextPageLink = animesEntity.HasNext
                ? CreateAnimesResourceUri(animesResourceParameter, ResourceUriType.NextPage)
                : null;
            var paginationMetaData = new
            {
                totalCount = animesEntity.TotalCount,
                pageSize = animesEntity.PageSize,
                currentPage = animesEntity.CurrentPage,
                totalPages = animesEntity.TotalPage,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(animesEntity);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAnime")]
        [AnimeResultFilterAttribute]
        public async Task<IActionResult> GetAnime(Guid id)
        {
            logger.LogInformation("Id: {Id}", id);
            var animeEntity = await animeRepository.GetAnimeById(id);
            if (animeEntity == null)
            {
                return NotFound();
            }

            return Ok(animeEntity);
        }

        [HttpPost]
        [AnimeResultFilter]
        public async Task<IActionResult> CreateAnime(AnimeForCreation animeForCreation)
        {
            logger.LogInformation("Request: {AnimeForCreation}", animeForCreation);
            // map it to entity that we'll save to db
            var entityToSave = mapper.Map<Entities.Anime>(animeForCreation);
            animeRepository.Save(entityToSave);
            await animeRepository.SaveChanges();
            return CreatedAtRoute("GetAnime", new { id = entityToSave.Id }, entityToSave);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAnime(Guid id, AnimeForUpdate animeForUpdate)
        {
            var animeFromRepo = await animeRepository.GetAnimeById(id);
            if (animeFromRepo == null)
            {
                return NotFound();
            }

            // copy over the request values and update the entity
            // for that
            // map the entity to Models.AnimeForUpdate
            // apply the updated fields values to AnimeForUpdate
            // map the AnimeForUpdate back to entity
            mapper.Map(animeForUpdate, animeFromRepo);
            animeRepository.Update(animeFromRepo);

            await animeRepository.SaveChanges();
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PartialUpdateAnime(Guid id,
            JsonPatchDocument<AnimeForUpdate> animeForPatchUpdate)
        {
            var animeFromRepo = await animeRepository.GetAnimeById(id);
            if (animeFromRepo == null)
            {
                return NotFound();
            }

            var animeToPatch = mapper.Map<AnimeForUpdate>(animeFromRepo);
            animeForPatchUpdate.ApplyTo(animeToPatch, ModelState);
            if (!TryValidateModel(animeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(animeToPatch, animeFromRepo);
            animeRepository.Update(animeFromRepo);
            await animeRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAnime(Guid id)
        {
            var anime = await animeRepository.GetAnimeById(id);
            if (anime == null)
            {
                return NotFound();
            }

            animeRepository.Delete(anime);
            await animeRepository.SaveChanges();
            return NoContent();
        }

        private string? CreateAnimesResourceUri(AnimesResourceParameter animesResourceParameter,
            ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAllAnime", new
                    {
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page - 1,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAllAnime", new
                    {
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page + 1,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
                default:
                    return Url.Link("GetAllAnime", new
                    {
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
            }
        }
    }
}