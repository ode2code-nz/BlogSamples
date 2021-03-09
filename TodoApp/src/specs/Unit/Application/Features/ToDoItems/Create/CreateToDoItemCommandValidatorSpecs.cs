using FluentValidation.TestHelper;
using ToDo.Api.Features.v1.ToDoItems;
using Specs.Library.ToDo.Extensions;

namespace Specs.Unit.ToDo.Application.Features.ToDoItems.Create
{
    public class CreateToDoItemCommandValidatorSpecs: ScenarioFor<CreateToDoItemCommandValidator>
    {
        public void Setup()
        {
            SUT = new CreateToDoItemCommandValidator();
        }

        public void Then_should_have_Title_with_no_more_than_200_characters()
        {
            SUT.ShouldHaveValidationErrorFor(x => x.Title, "a".Repeat(201))
                .WithErrorMessage("The length of 'Title' must be 200 characters or fewer. You entered 201 characters.");
            SUT.ShouldNotHaveValidationErrorFor(x => x.Title, "a".Repeat(100));

        }
    }
}
