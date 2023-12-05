using FluentValidation;
using PicPaySimplificado.Application.Commands;
using PicPaySimplificado.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Validators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private const string MinLengthErrorMessage = "{PropertyName} must have at least {MinLength} characters";
        private const string MaxLengthErrorMessage = "{PropertyName} must not reach {MaxLength} characters";
        private const string EmptyStringErrorMessage = "{PropertyName} can not be empty";
        private const string ErrorMessage = "Invalid {PropertyName}";
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(EmptyStringErrorMessage)
                .MinimumLength(Name.MIN_LENGTH).WithMessage(MinLengthErrorMessage);
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(EmptyStringErrorMessage)
                .MinimumLength(Name.MIN_LENGTH).WithMessage(MinLengthErrorMessage);
            RuleFor(x => x.Email)
                .NotEmpty().NotEmpty().WithMessage(EmptyStringErrorMessage)
                .MinimumLength(Email.ADDRESS_MIN_LENGTH).WithMessage(MinLengthErrorMessage)
                .MaximumLength(Email.ADDRESS_MAX_LENGTH).WithMessage(MaxLengthErrorMessage);
            RuleFor(x => x.Document)
                .NotEmpty().WithMessage(EmptyStringErrorMessage)
                .MinimumLength(Document.CPF_MAX_LENGTH).WithMessage(MinLengthErrorMessage)
                .MaximumLength(Document.CNPJ_MAX_LENGTH).WithMessage(MaxLengthErrorMessage);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(EmptyStringErrorMessage)
                .MinimumLength(Password.PASSWORD_MIN_LENGTH).WithMessage(MinLengthErrorMessage)
                .Must(ValidatePassword).WithMessage(ErrorMessage);
            RuleFor(x => x.Balance)
                .GreaterThan(0).WithMessage(ErrorMessage);
            RuleFor(x => x.UserType)
                .NotNull().WithMessage(ErrorMessage);
        }

        private bool ValidatePassword(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 8) return false;

            return Regex.IsMatch(value, "^(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~])(?=.*[A-Z])(?=.*\\d).+$");
        }
    }
}
