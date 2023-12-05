using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Events
{
    public class UserUpdated : Event
    {
        public UserUpdated(
            Guid id, 
            string firstName, 
            string lastName, 
            string email, 
            string document, 
            string password, 
            decimal balance, 
            int userType)
        {
            AgregateId = id;
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
    }
}
