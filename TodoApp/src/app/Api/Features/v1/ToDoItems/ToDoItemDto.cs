using AutoMapper;
using Todo.Api.Common.Mappings;
using Todo.Domain.Model.ToDos;
using Todo.SharedModels.v1.ToDoItems;

namespace Todo.Api.Features.v1.ToDoItems
{
    public class ToDoItemDto : IMapFrom<ToDoItem>, IMapToAndFrom<ToDoItemResponse>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public bool Done { get; set; }

        public void MapFrom(Profile profile)
        {
            profile.CreateMap<ToDoItem, ToDoItemDto>()
                .ForMember(d => d.Done,
                    opt => opt.MapFrom(s => s.IsDone));
        }
    }
}
