using Area92.Entities;
using Area92.Helpers;
using Area92.ResourceParameters;

namespace Area92.Services
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAnimes();
        Task<PagedList<Anime>> GetAnimes(AnimesResourceParameter animesResourceParameter);
        Task<IEnumerable<Anime>> GetAnimes(IEnumerable<Guid> ids);
        Task<Anime> GetAnimeById(Guid id);
        void Update(Anime anime);
        void Delete(Anime anime);
        void Save(Anime anime);
        Task<bool> SaveChanges();
    }
}
