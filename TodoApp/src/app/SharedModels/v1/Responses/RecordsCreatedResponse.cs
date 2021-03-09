using System.Collections.Generic;

namespace ToDo.SharedModels.v1.Responses
{
    public class RecordsCreatedResponse
    {
        public RecordsCreatedResponse() { }

        public RecordsCreatedResponse(int newId)
        {
            NewIds.Add(newId);
            Message = $"Record created with Id {newId}";
        }

        public RecordsCreatedResponse(List<int> newIds)
        {
            NewIds = newIds;
            Message = "Records created";
        }

        public List<int> NewIds { get; set; } = new List<int>();
        public string Message { get; set; }
    }
}