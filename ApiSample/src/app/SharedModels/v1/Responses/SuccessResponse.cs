namespace ApiSample.SharedModels.v1.Responses
{
    public class SuccessResponse<T> 
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public SuccessResponse() { }

        public SuccessResponse(T response, string message = "Success")
        {
            Message = message;
            Data = response;
        }
    }
}