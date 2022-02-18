using Area92.Context;
using Area92.Helpers;
using Area92.Models;
using Area92.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using Anime = Area92.Entities.Anime;

namespace Area92.Services
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimesContext _animeContext;
        private readonly IPropertyMappingService _propertyMappingService;

        public AnimeRepository(AnimesContext animeContext, IPropertyMappingService propertyMappingService)
        {
            this._animeContext = animeContext ?? throw new ArgumentNullException(nameof(animeContext));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void Delete(Anime anime)
        {
            _animeContext.Remove(anime);
        }

        public async Task<Anime> GetAnimeById(Guid id)
        {
            return await _animeContext.Animes.FindAsync(id);
        }

        public async Task<IEnumerable<Anime>> GetAllAnime()
        {
            return await _animeContext.Animes.ToListAsync();
        }

        public async Task<PagedList<Anime>> GetAllAnime(AnimesResourceParameter animeResourceParameter)
        {
            var animes = _animeContext.Animes as IQueryable<Anime>;
            // releaseYear exists but not searchQuery
            if (animeResourceParameter.ReleaseYear != 0)
            {
                animes = animes.Where(anime => anime.ReleaseYear == animeResourceParameter.ReleaseYear);
            }
            // searchQuery exists but not releaseYear
            if (!string.IsNullOrWhiteSpace(animeResourceParameter.SearchQuery))
            {
                var searchQuery = animeResourceParameter.SearchQuery.Trim();
                animes = animes.Where(anime => anime.GenresString.Contains(searchQuery) || anime.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(animeResourceParameter.OrderBy))
            {
                var propertyMappingValues = _propertyMappingService.GetPropertyMapping<AnimeForCreation, Anime>();
                animes = animes.ApplySort(animeResourceParameter.OrderBy, propertyMappingValues);
            }
            
            return PagedList<Anime>.create(animes, animeResourceParameter.Page, animeResourceParameter.Size);
        }

        public async Task<IEnumerable<Anime>> GetAllAnime(IEnumerable<Guid> ids)
        {
            return await _animeContext.Animes.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public void Save(Anime anime)
        {
            _animeContext.Add(anime);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _animeContext.SaveChangesAsync() > 0);
        }

        public void Update(Anime anime)
        {
            _animeContext.Entry(anime).State = EntityState.Modified;
        }
    }
}
