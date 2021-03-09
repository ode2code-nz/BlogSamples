namespace ToDo.SharedModels.v1.ToDoItems
{
    public class ToDoItemResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public bool Done { get; set; }
    }
}
