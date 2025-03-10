using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs.Authentication
{
    public class CreateUser: BaseUserModel
    {
        public string? fullName { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
