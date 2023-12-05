using PicPaySimplificado.Application.Validators;
using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Commands
{
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(string firstName, string lastName, string email, string document, string password, decimal balance, int userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Document = document;
            Password = password;
            Balance = balance;
            UserType = userType;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Document { get; private set; }
        public string Password { get; private set; }
        public decimal Balance { get; private set; }
        public int UserType { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
