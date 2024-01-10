using Contracts.Book;
using FluentValidation;

namespace Contracts.Validation.Book
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(e => e.PageSize).GreaterThan(0)
                .WithMessage("PageSize must be greater than 0");
            RuleFor(e => e.PageNumber).GreaterThan(-1)
                .WithMessage("PageSize must be greater or equal to 0");
        }
    }
}