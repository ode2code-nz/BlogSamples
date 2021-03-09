using Todo.SharedModels.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Todo.Api.Common.Web.Examples.Responses
{
    public class RecordsCreatedResponseExample : IExamplesProvider<RecordsCreatedResponse>
    {
        public RecordsCreatedResponse GetExamples()
        {
            return new RecordsCreatedResponse(39);
        }
    }
}