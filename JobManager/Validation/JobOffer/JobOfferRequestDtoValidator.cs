using FluentValidation;
using JobManager.Core.Data.DataTransferObjects.Request.JobOffer;

namespace JobManager.Validation.JobOffer
{
    public class JobOfferRequestDtoValidator : AbstractValidator<JobOfferRequestDto>
    {
        public JobOfferRequestDtoValidator()
        {
            RuleFor(x => x.CategoryIds)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty()
                .Must(x => x.Length >= 4 && x.Length <= 2048)
                    .WithMessage("The description has to be between 4 and 2048 characters!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(x => x.Length >= 2 && x.Length <= 24)
                    .WithMessage("The name has to be between 2 and 24 characters!");

            RuleFor(x => x.PayPerHour)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
