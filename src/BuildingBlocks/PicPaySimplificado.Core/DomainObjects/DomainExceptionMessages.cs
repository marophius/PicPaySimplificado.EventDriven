using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.DomainObjects
{
    public static class DomainExceptionMessages
    {
        public const string BalanceErrorMessage = "Balance cannot be negative";
        public const string AmountErrorMessage = "Amount must be greater than zero";
        public const string MerchantErrorMessage = "Only Merchant users can send money";
        public const string TransactionValidateErrorMessage = "Value must be greater than zero";
        public const string DocumentValidateErrorMessage = "Invalid Document";
        public const string AddressErrorMessage = "Invalid E-mail address.";
        public const string NameValidateErrorMessage = "Invalid Name";
        public const string PasswordValidateErrorMessage = "Invalid Password";
    }
}
