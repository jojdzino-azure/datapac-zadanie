using Contracts.Book;
using Domain.Repositories.Book;
using FluentValidation;

namespace Contracts.Validation.Book
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(IQueryBookRepository queryBookRepository)
        {
            RuleFor(e => e.Id).GreaterThan(0)
                .WithMessage("Is must be greater than 0")
                .MustAsync(async (id, token) =>
                {
                    var book = await queryBookRepository.GetBookAsync(id, token);
                    return book != null;
                })
                .WithMessage((x) => $"Book with id {x.Id} does not exist.");

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