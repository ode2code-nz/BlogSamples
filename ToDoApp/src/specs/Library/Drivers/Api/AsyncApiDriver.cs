using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToDo.Api.Common.Paging;
using ToDo.Infrastructure.Identity.Models;
using Specs.Library.ToDo.Builders;

namespace Specs.Library.ToDo.Drivers.Api
{
    public class AsyncApiDriver
    {
        public ITestHost Server { get; }

        public AsyncApiDriver(ITestHost server)
        {
            Server = server;
        }

        public HttpClient Client
        {
            get
            {
                var client = Server.Client;
                client.DefaultRequestHeaders.Authorization = RunAsIdentity.IsAuthenticated 
                    ? new AuthenticationHeaderValue("Bearer", RunAsIdentity.Token) : null;

                return client;
            }
        }

        /// <summary>
        /// The user identity that the HttpClient will call the API with
        /// </summary>
        public AuthenticationModel RunAsIdentity { get; set; } = Get.User.Administrator;

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string requestUri)
        {
            // var request = Get.MotherFor.HttpRequestMessage.Get(requestUri);
            var response = await Client.GetAsync(requestUri);
            Client.DefaultRequestHeaders.Authorization = null;
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> GetWithCheckAsync<TResponse>(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetAsync<TResponse>(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(GetWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<PagedList<TResponse>>> GetAllPagedAsync<TResponse>(string requestUri)
        {
            var response = await Client.GetAsync(requestUri);

            return new ApiResponse<PagedList<TResponse>>(response);
        }

        public async Task<ApiResponse<List<TResponse>>> GetAllAsync<TResponse>(string requestUri)
        {
          //  Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjYwOWY4YjQ2LWJiYmMtNGRhOS04NmQ3LWJiZGYwN2FiMzhhYiIsImVtYWlsIjoiYWRtaW5AZXhhbXBsZS5jb20iLCJ1aWQiOiJlZDE5Y2M0ZC1hMDNiLTQ2N2EtYjcxNC01ZmU5ZTMwOWU2MDUiLCJyb2xlcyI6IkFkbWluaXN0cmF0b3IiLCJleHAiOjE2MTE0NDY3ODMsImlzcyI6IlNlY3VyZUFwaSIsImF1ZCI6IlNlY3VyZUFwaVVzZXIifQ.LFEZl-t0GHoDZD0KDEajuHWLm1zrUWPJKuCpF1X1fnY");
            var response = await Client.GetAsync(requestUri);

            return new ApiResponse<List<TResponse>>(response);
        }

        public async Task<ApiResponse<List<TResponse>>> GetAllWithCheckAsync<TResponse>(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetAllAsync<TResponse>(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(GetAllWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TResponse>(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PostWithCheckAsync<TResponse>(string requestUri, object resource, HttpStatusCode expectedStatusCode = HttpStatusCode.Created)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(PostAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse> PostAsync(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PostAsync(requestUri, content);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse<TResponse>> PutAsync<TResponse>(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PutAsync(requestUri, content);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PutWithCheckAsync<TResponse>(string requestUri, object resource, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await PutAsync<TResponse>(requestUri, resource);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(PutWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse> PutAsync(string requestUri, object resource)
        {
            var content = resource.AsJsonContent();
            var response = await Client.PutAsync(requestUri, content);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteAsync(string requestUri)
        {
            var response = await Client.DeleteAsync(requestUri);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> DeleteWithCheckAsync(string requestUri, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var response = await DeleteAsync(requestUri);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(DeleteWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse<TResponse>> SendAsync<TResponse>(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);
            return new ApiResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> SendWithCheckAsync<TResponse>(HttpRequestMessage request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await SendAsync<TResponse>(request);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(SendWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }

        public async Task<ApiResponse> SendAsync(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);
            return new ApiResponse(response);
        }

        public async Task<ApiResponse> SendWithCheckAsync(HttpRequestMessage request, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await SendAsync(request);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new ApiDriverException($"{nameof(SendWithCheckAsync)} received '{response.StatusCode}' but expected '{expectedStatusCode}'");
            }

            return response;
        }
    }
}