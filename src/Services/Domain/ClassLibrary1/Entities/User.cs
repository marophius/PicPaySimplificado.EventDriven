using PicPaySimplificado.Core.DomainObjects;
using PicPaySimplificado.Domain.Enums;
using PicPaySimplificado.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.Entities
{
    public class User : Entity, IAgregateRoot
    {
        protected User() { }

        public User(
            string firstName,
            string lastName,
            string document,
            string email,
            string password,
            decimal balance,
            int userType
            )
        {
            Name = new Name(firstName, lastName);
            Email = new Email(email);
            Password = new Password(password);
            UpdateUserType(userType, document);
            ValidateBalance(balance);
            RegisterDate = DateTimeOffset.Now;
        }

        public User(
            Guid id,
            string firstName,
            string lastName,
            string document,
            string email,
            string password,
            decimal balance,
            int userType,
            DateTimeOffset registerDate
            )
        {
            Id = id;
            Name = new Name(firstName, lastName);
            Email = new Email(email);
            Password = new Password(password);
            UpdateUserType(userType, document);
            ValidateBalance(balance);
            RegisterDate = DateTimeOffset.Now;
            RegisterDate = registerDate;
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        private List<Transaction> _transactionsAsPayer = new List<Transaction>();
        private List<Transaction> _transactionsAsPayee = new List<Transaction>();
        public IReadOnlyCollection<Transaction> TransactionsAsPayer => _transactionsAsPayer;
        public IReadOnlyCollection<Transaction> TransactionsAsPayee => _transactionsAsPayee;
        public decimal Balance { get; private set; }
        public EUserType UserType { get; private set; }
        public DateTimeOffset RegisterDate { get; private set; }

        public void FullUpdate(
            string firstName,
            string lastName,
            string document,
            string email,
            string password,
            decimal balance,
            int userType)
        {
            UpdateName(firstName, lastName);
            UpdateEmail(email);
            UpdatePassword(password);
            UpdateUserType(userType, document);
            ValidateBalance(balance);
        }
        public void UpdateName(string firstName, string lastName)
        {
            Name = new Name(firstName, lastName);
        }

        public void UpdateDocument(string document)
        {
            Document = new Document(document, (int)UserType);
        }

        public void UpdateEmail(string email)
        {
            Email = new Email(email);
        }

        public void UpdatePassword(string password)
        {
            Password = new Password(password);
        }

        public void UpdateUserType(int userType, string number)
        {
            UserType = userType switch
            {
                0 => EUserType.Common,
                1 => EUserType.Merchant,
                _ => EUserType.Common
            };

            UpdateDocument(number);
        }

        public void SendMoney(Transaction trans)
        {
            if (trans.Value <= 0)
                throw new DomainException(DomainExceptionMessages.AmountErrorMessage);
            var oldBalance = Balance;
            if (UserType == EUserType.Common)
                throw new DomainException(DomainExceptionMessages.MerchantErrorMessage);
            oldBalance -= trans.Value;
            if (!ValidateBalance(oldBalance))
            {
                throw new DomainException(DomainExceptionMessages.BalanceErrorMessage);
            }

            _transactionsAsPayer.Add(trans);
        }

        public void ReceiveMoney(Transaction trans)
        {
            if (trans.Value <= 0)
            {
                throw new DomainException(DomainExceptionMessages.AmountErrorMessage);
            }

            Balance += trans.Value;
            _transactionsAsPayee.Add(trans);
        }

        private bool ValidateBalance(decimal balance)
        {
            if (balance < 0)
            {
                return false;
            }
            Balance = balance;

            return true;
        }
    }
}
