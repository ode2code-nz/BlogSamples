namespace ApiSample.SharedModels.v1.ToDoItems
{
    public class CreateToDoItemRequest 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public bool IgnoreWarnings { get; set; } = false;
    }
}