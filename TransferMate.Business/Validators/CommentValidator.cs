using FluentValidation;

namespace TransferMate.Business.Validators
{
    public class CommentValidator : AbstractValidator<Data.Models.Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentContent).MinimumLength(3).MaximumLength(1500).NotEmpty().NotNull();
            RuleFor(x => x.ReminderDate).NotNull().NotEmpty();
            RuleFor(x => x.CreatedDate).NotNull().NotEmpty();
            RuleFor(x => x.CommentType).NotNull().NotEmpty();
        }
    }
}
