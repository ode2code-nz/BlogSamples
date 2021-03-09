using AutoMapper;

namespace ToDo.Api.Common.Mappings
{
    public interface IMapToAndFrom<T>
    {
        void MapToAndFrom(Profile profile)
        {
            profile
                .CreateMap(GetType(), typeof(T))
                .ReverseMap();
        }
    }
}