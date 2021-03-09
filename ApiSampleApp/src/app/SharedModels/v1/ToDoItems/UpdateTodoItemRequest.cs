namespace ApiSample.SharedModels.v1.ToDoItems
{
    public class UpdateToDoItemRequest 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsDone { get; set; }

        public bool IgnoreWarnings { get; set; } = false;

    }
}