using Todo.SharedModels.v1.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace Todo.Api.Common.Web.Examples.Responses
{
    public class ToDoItemResponseExample : IExamplesProvider<ToDoItemResponse>
    {
        public ToDoItemResponse GetExamples()
        {
            return new ToDoItemResponse
            {
                Id = 39,
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "tests are important",
                Done = true
            };
        }
    }
}