using Contracts.Book;
using FluentValidation;

namespace Contracts.Validation.Book
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.Book.Name).MaximumLength(256).NotEmpty();
            RuleFor(b => b.Book.Description).MaximumLength(4000).NotEmpty();
            RuleFor(b => b.Book.PublishedOn)
                .NotEmpty()
                .WithMessage("PublishedOn is empty")

                .Must(e => e.CompareTo(DateTime.Parse("1.1.1900")) > 0 ? true : false)
                .WithMessage("Date must be after 1900");
            RuleFor(b => b.Book.AuthorName).MaximumLength(256).NotEmpty().WithMessage("AuthorName must be filled out");
        }
    }
}