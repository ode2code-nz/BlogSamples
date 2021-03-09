using ToDo.SharedModels.v1.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace ToDo.Api.Common.Web.Examples.Requests
{
    public class CreateToDoItemRequestExample : IExamplesProvider<CreateToDoItemRequest>
    {
        public CreateToDoItemRequest GetExamples()
        {
            return new CreateToDoItemRequest
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "joe@email.com"
            };
        }
    }
}