using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries.DTOs
{
    public record UserDTO(
        Guid Id,
        string FirstName, 
        string LastName, 
        string Email, 
        decimal Balance);
}
