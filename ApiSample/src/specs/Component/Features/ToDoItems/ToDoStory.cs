using Specify.Stories;

namespace Specs.Component.ApiSample.Features.ToDoItems
{
    public class ToDoStory : UserStory
    {
        public ToDoStory()
        {
            AsA = "As a User";
            IWant = "I want to view my list of things I want to do";
            SoThat = "So that I can decide what to do today.";
        }
    }
}