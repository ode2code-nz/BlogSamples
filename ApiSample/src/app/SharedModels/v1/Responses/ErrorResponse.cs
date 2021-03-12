using System.Collections.Generic;
using System.Text;

namespace ApiSample.SharedModels.v1.Responses
{
    public class ErrorResponse
    {
        public string ErrorId { get; set; }
        public short StatusCode { get; set; }

        public string Title { get; set; }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public ErrorResponse() { }
    }

    public class ErrorModel
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
        public int RowKey { get; set; }

        public ErrorModel(string propertyName, string message, int rowKey = int.MinValue)
        {
            PropertyName = propertyName;
            Message = message;
            RowKey = rowKey;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"  Property: {PropertyName}");
            sb.AppendLine($"  Message: {Message}");
            sb.AppendLine($"  RowKey: {RowKey}");
            return sb.ToString();
        }
    }
}