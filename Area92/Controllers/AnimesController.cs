using System.Text.Json;
using Area92.Filters;
using Area92.Helpers;
using Area92.Models;
using Area92.ResourceParameters;
using Area92.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Anime = Area92.Entities.Anime;

namespace Area92.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    // [Route("api/v{v:apiVersion}/animes")]
    [Route("api/animes")]
    // [Authorize]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public AnimesController(IAnimeRepository animeRepository,
            ILogger<AnimesController> logger,
            IMapper mapper,
            IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService)
        {
            this._animeRepository = animeRepository ?? throw new ArgumentNullException(nameof(animeRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService =
                propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService;
        }

        [HttpGet(Name = "GetAllAnime")]
        [AnimesResultFilterAttribute]
        [Authorize]
        public async Task<IActionResult> GetAllAnime([FromQuery] AnimesResourceParameter animesResourceParameter)
        {
            _logger.LogInformation("Get Request for All Anime: {AnimesResourceParameter}", animesResourceParameter);

            if (!_propertyMappingService
                    .ValidMappingExistsFor<AnimeForCreation, Anime>(animesResourceParameter.OrderBy))
            {
                return BadRequest();
            }

            var animesEntity = await _animeRepository.GetAllAnime(animesResourceParameter);

            _logger.LogInformation("Response from db {Response}", animesEntity);

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
            var links = GenerateLinksForAllAnime(animesResourceParameter);
            var shapedAnimes = animesEntity.ShapeData(animesResourceParameter.Fields);
            var shapedAnimesWithLinks = shapedAnimes.Select(anime =>
            {
                var animeDictionary = anime as IDictionary<string, object>;
                animeDictionary.Add("links", GenerateLinksForAnime((Guid)animeDictionary["Id"], null));
                return animeDictionary;
            });
            var responseDictionary = new Dictionary<string, object>
            {
                { "value", shapedAnimesWithLinks },
                { "links", links }
            };
            _logger.LogInformation("Response {Response}", responseDictionary);
            return Ok(animesEntity);
        }

        [HttpGet]
        [Route("{id}", Name = "GetAnime")]
        [AnimeResultFilterAttribute]
        public async Task<IActionResult> GetAnime(Guid id, string fields)
        {
            _logger.LogInformation("Request Id: {Id}", id);
            if (!_propertyCheckerService.PropertyExists<Anime>(fields))
            {
                return BadRequest();
            }

            var animeEntity = await _animeRepository.GetAnimeById(id);
            if (animeEntity == null)
            {
                return NotFound();
            }

            var links = GenerateLinksForAnime(id, fields);
            var response = animeEntity.ShapeDataAnime(fields) as IDictionary<string, object>;
            response.Add("links", links);
            return Ok(response);
        }

        [HttpPost]
        [AnimeResultFilter]
        public async Task<IActionResult> CreateAnime(AnimeForCreation animeForCreation)
        {
            _logger.LogInformation("Request: {AnimeForCreation}", animeForCreation);
            // map it to entity that we'll save to db
            var entityToSave = _mapper.Map<Anime>(animeForCreation);
            _animeRepository.Save(entityToSave);
            await _animeRepository.SaveChanges();
            _logger.LogInformation("Saved in db: {Anime}", entityToSave);
            // Get the links
            var links = GenerateLinksForAnime(entityToSave.Id, null);

            // To add the links we need expandoObject
            // so, we shape the data and add links
            var shapeDataAnime = entityToSave.ShapeDataAnime(null) as IDictionary<string, object>;
            _logger.LogInformation("Response after shaping data: {ShapeDataAnime}", shapeDataAnime);
            shapeDataAnime.Add("links", links);
            return CreatedAtRoute("GetAnime", new { id = shapeDataAnime["Id"] }, shapeDataAnime);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAnime(Guid id, AnimeForUpdate animeForUpdate)
        {
            var animeFromRepo = await _animeRepository.GetAnimeById(id);

            // copy over the request values and update the entity
            // for that
            // map the entity to Models.AnimeForUpdate
            // apply the updated fields values to AnimeForUpdate
            // map the AnimeForUpdate back to entity
            _mapper.Map(animeForUpdate, animeFromRepo);
            _animeRepository.Update(animeFromRepo);

            await _animeRepository.SaveChanges();
            return NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PartialUpdateAnime(Guid id,
            JsonPatchDocument<AnimeForUpdate> animeForPatchUpdate)
        {
            var animeFromRepo = await _animeRepository.GetAnimeById(id);
            if (animeFromRepo == null)
            {
                return NotFound();
            }

            var animeToPatch = _mapper.Map<AnimeForUpdate>(animeFromRepo);
            animeForPatchUpdate.ApplyTo(animeToPatch, ModelState);
            if (!TryValidateModel(animeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(animeToPatch, animeFromRepo);
            _animeRepository.Update(animeFromRepo);
            await _animeRepository.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteAnime")]
        public async Task<IActionResult> DeleteAnime(Guid id)
        {
            var anime = await _animeRepository.GetAnimeById(id);
            if (anime == null)
            {
                return NotFound();
            }

            _animeRepository.Delete(anime);
            await _animeRepository.SaveChanges();
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
                        fields = animesResourceParameter.Fields,
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page - 1,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAllAnime", new
                    {
                        fields = animesResourceParameter.Fields,
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page + 1,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link("GetAllAnime", new
                    {
                        fields = animesResourceParameter.Fields,
                        orderBy = animesResourceParameter.OrderBy,
                        page = animesResourceParameter.Page,
                        size = animesResourceParameter.Size,
                        releaseYear = animesResourceParameter.ReleaseYear,
                        searchQuery = animesResourceParameter.SearchQuery
                    });
            }
        }

        public IEnumerable<LinkDto> GenerateLinksForAnime(Guid id, string fields)
        {
            var links = new List<LinkDto>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                // If the fields is null or whitespace then self
                links.Add(new LinkDto(Url.Link("GetAnime", new { id }), "self", "GET"));
            }
            else
            {
                links.Add(new LinkDto(Url.Link("GetAnime", new { id, fields }), "self", "GET"));
            }

            links.Add(new LinkDto(Url.Link("DeleteAnime", new { id }), "delete_anime", "DELETE"));

            return links;
        }

        public IEnumerable<LinkDto> GenerateLinksForAllAnime(AnimesResourceParameter animesResourceParameter)
        {
            var links = new List<LinkDto>();
            links.Add(new LinkDto(CreateAnimesResourceUri(animesResourceParameter, ResourceUriType.CurrentPage), "self",
                "GET"));
            return links;
        }
    }
}