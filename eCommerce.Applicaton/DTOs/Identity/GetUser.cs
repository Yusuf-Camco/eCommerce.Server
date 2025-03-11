using eCommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs.Identity
{
    public class GetUser
    {
        public Guid Id { get; set; }
        public string? fullName { get; set; }
    }
}
