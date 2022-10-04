using FluentValidation;

namespace TransferMate.Business.Validators
{
    public class TaskValidator : AbstractValidator<Data.Models.Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title).MinimumLength(3).MaximumLength(50).NotEmpty().NotNull();
            RuleFor(x => x.TaskStatus).NotNull().NotEmpty();
            RuleFor(x => x.AssignedTo).NotNull().NotEmpty();
            RuleFor(x => x.NextActionDate).NotNull().NotEmpty();
            RuleFor(x => x.TaskDescription).MinimumLength(3).MaximumLength(1500).NotEmpty().NotNull();
            RuleFor(x => x.TaskType).NotNull().NotEmpty();
        }
    }
}
