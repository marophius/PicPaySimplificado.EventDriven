using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 30;

        protected Name() { }

        public Name(string firstName, string lastName)
        {
            if(Validate(firstName) && Validate(lastName))
            {
                FirstName = firstName;
                LastName = lastName;
            }else
            {
                throw new DomainException(DomainExceptionMessages.NameValidateErrorMessage);
            }
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        private bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < MIN_LENGTH || value.Length > MAX_LENGTH) return false;

            return Regex.IsMatch(value, "^(?=.*[A-ZÀ-ÿ~])(?=.*[a-zà-ÿ~])[A-Za-zÀ-ÿ~]+$");
        }
    }
}
