using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public const int ADDRESS_MAX_LENGTH = 254;
        public const int ADDRESS_MIN_LENGTH = 5;
        public string Address { get; private set; }

        protected Email() { }

        public Email(string address)
        {
            if (Validate(address))
                Address = address;
            else
                throw new DomainException(DomainExceptionMessages.AddressErrorMessage);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        private bool Validate(string email)
        {
            if (email.Length < ADDRESS_MIN_LENGTH || email.Length > ADDRESS_MAX_LENGTH)
                return false;
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
