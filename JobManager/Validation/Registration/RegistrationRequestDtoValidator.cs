using FluentValidation;
using JobManager.Core.Data.DataTransferObjects.Request.Registration;
using System;

namespace JobManager.Validation.Registration
{
    public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestDtoValidator()
        {
            RuleFor(registrationRequestDto => registrationRequestDto.Username)
                .NotEmpty()
                .MaximumLength(16)
                .MinimumLength(1);

            RuleFor(registrationRequestDto => registrationRequestDto.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(registrationRequestDto => registrationRequestDto.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64);

            RuleFor(registrationRequestDto => registrationRequestDto.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Now.AddYears(-12))
                    .WithMessage("You have to be atleast 12 years old in order to register!"); // TODO: Translation
        }
    }
}
