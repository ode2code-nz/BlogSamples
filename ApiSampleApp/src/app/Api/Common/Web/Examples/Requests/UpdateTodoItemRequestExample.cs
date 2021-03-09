using ApiSample.SharedModels.v1.ToDoItems;
using Swashbuckle.AspNetCore.Filters;

namespace ApiSample.Api.Common.Web.Examples.Requests
{
    public class UpdateToDoItemRequestExample : IExamplesProvider<UpdateToDoItemRequest>
    {
        public UpdateToDoItemRequest GetExamples()
        {
            return new UpdateToDoItemRequest
            {
                Id = 1,
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing.",
                Email = "joe@email.com"
            };
        }
    }
}