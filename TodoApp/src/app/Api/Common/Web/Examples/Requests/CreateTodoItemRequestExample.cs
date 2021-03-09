using Todo.SharedModels.v1.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace Todo.Api.Common.Web.Examples.Requests
{
    public class CreateTodoItemRequestExample : IExamplesProvider<CreateTodoItemRequest>
    {
        public CreateTodoItemRequest GetExamples()
        {
            return new CreateTodoItemRequest
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "joe@email.com"
            };
        }
    }
}