using Area92.Helpers;
using AutoMapper;

namespace Area92.Profiles
{
    public class AnimesProfile : Profile
    {
        public AnimesProfile()
        {
            // using this at return time to modify the response
            // getting the entity from db
            // src = entity, dst = models
            CreateMap<Entities.Anime, Models.Anime>();
            // .ForMember(dst => dst.Genres
            //     , opt => opt.MapFrom(src => src.GenresString));
            // .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear + 10));

            // using this to map from request to entity to save it to db
            // src - AnimeForCreation (request)
            // map the endYear from request and find IsEnded which we set in destination (Anime)
            CreateMap<Models.AnimeForCreation, Entities.Anime>()
                .ForMember(
                    dst => dst.IsEnded, // set this value
                    opt => opt.MapFrom(src => src.ReleaseYear.IsEndedSeries(src.EndYear)));

            CreateMap<Models.AnimeForUpdate, Entities.Anime>()
                .ForMember(
                    dst => dst.IsEnded, // set this value
                    opt => opt.MapFrom(src => src.ReleaseYear.IsEndedSeries(src.EndYear)));

            // CreateMap<Models.AnimeForUpdate, Entities.Anime>();
            CreateMap<Entities.Anime, Models.AnimeForUpdate>();
        }
    }
}