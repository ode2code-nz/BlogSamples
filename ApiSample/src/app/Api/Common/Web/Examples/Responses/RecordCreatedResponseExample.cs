using ApiSample.SharedModels.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace ApiSample.Api.Common.Web.Examples.Responses
{
    public class RecordsCreatedResponseExample : IExamplesProvider<RecordsCreatedResponse>
    {
        public RecordsCreatedResponse GetExamples()
        {
            return new RecordsCreatedResponse(39);
        }
    }
}