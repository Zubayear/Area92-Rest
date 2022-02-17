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
        private readonly AnimesContext animesContext;
        private readonly IPropertyMappingService _propertyMappingService;

        public AnimeRepository(AnimesContext animesContext, IPropertyMappingService propertyMappingService)
        {
            this.animesContext = animesContext ?? throw new ArgumentNullException(nameof(animesContext));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void Delete(Anime anime)
        {
            animesContext.Remove(anime);
        }

        public async Task<Anime> GetAnimeById(Guid id)
        {
            return await animesContext.Animes.FindAsync(id);
        }

        public async Task<IEnumerable<Anime>> GetAnimes()
        {
            return await animesContext.Animes.ToListAsync();
        }

        public async Task<PagedList<Anime>> GetAnimes(AnimesResourceParameter animesResourceParameter)
        {
            var animes = animesContext.Animes as IQueryable<Anime>;
            // releaseYear exists but not searchQuery
            if (animesResourceParameter.ReleaseYear != 0)
            {
                animes = animes.Where(anime => anime.ReleaseYear == animesResourceParameter.ReleaseYear);
            }
            // searchQuery exists but not releaseYear
            if (!string.IsNullOrWhiteSpace(animesResourceParameter.SearchQuery))
            {
                var searchQuery = animesResourceParameter.SearchQuery.Trim();
                animes = animes.Where(anime => anime.GenresString.Contains(searchQuery) || anime.Title.Contains(searchQuery));
            }

            if (!string.IsNullOrWhiteSpace(animesResourceParameter.OrderBy))
            {
                var propertyMappingValues = _propertyMappingService.GetPropertyMapping<AnimeForCreation, Anime>();
                animes = animes.ApplySort(animesResourceParameter.OrderBy, propertyMappingValues);
            }
            
            return PagedList<Anime>.create(animes, animesResourceParameter.Page, animesResourceParameter.Size);
        }

        public async Task<IEnumerable<Anime>> GetAnimes(IEnumerable<Guid> ids)
        {
            return await animesContext.Animes.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public void Save(Anime anime)
        {
            animesContext.Add(anime);
        }

        public async Task<bool> SaveChanges()
        {
            return (await animesContext.SaveChangesAsync() > 0);
        }

        public void Update(Anime anime)
        {
            animesContext.Entry(anime).State = EntityState.Modified;
        }
    }
}
