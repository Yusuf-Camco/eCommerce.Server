﻿using eCommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.Interfaces.Authentications
{
    public interface IRoleManagement
    {
        Task<string> GetUserRole(string userEmail);
        Task<bool> AddUserToRole(AppUser user, string roleName);
    } 
}
