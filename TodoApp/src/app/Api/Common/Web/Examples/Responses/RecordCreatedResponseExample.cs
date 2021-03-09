using ToDo.SharedModels.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace ToDo.Api.Common.Web.Examples.Responses
{
    public class RecordsCreatedResponseExample : IExamplesProvider<RecordsCreatedResponse>
    {
        public RecordsCreatedResponse GetExamples()
        {
            return new RecordsCreatedResponse(39);
        }
    }
}