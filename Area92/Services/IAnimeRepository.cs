using Area92.Entities;
using Area92.Helpers;
using Area92.ResourceParameters;

namespace Area92.Services
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAnime();
        Task<PagedList<Anime>> GetAllAnime(AnimesResourceParameter animeResourceParameter);
        Task<IEnumerable<Anime>> GetAllAnime(IEnumerable<Guid> ids);
        Task<Anime> GetAnimeById(Guid id);
        void Update(Anime anime);
        void Delete(Anime anime);
        void Save(Anime anime);
        Task<bool> SaveChanges();
    }
}
