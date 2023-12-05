using PicPaySimplificado.Application.Queries.DTOs;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries.Extensions
{
    public static class UserExtensions
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO(user.Id, user.Name.FirstName, user.Name.LastName, user.Email.Address, user.Balance);
        }

        public static List<UserDTO> ToDTOList(this List<User> users)
        {
            return users.Select(x => x.ToDTO()).ToList();
        }
    }
}
