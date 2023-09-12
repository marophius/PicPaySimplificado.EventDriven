using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        public const int PASSWORD_MIN_LENGTH = 8;

        protected Password() { }

        internal Password(string value)
        {
            if (Validate(value))
            {
                Value = value;
            }
            else
            {
                throw new DomainException(DomainExceptionMessages.PasswordValidateErrorMessage);
            }
        }
        public string Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
           yield return Value;
        }

        private bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < PASSWORD_MIN_LENGTH) return false;

            return Regex.IsMatch(value, "^(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~])(?=.*[A-Z])(?=.*\\d).+$");
        }
    }
}
