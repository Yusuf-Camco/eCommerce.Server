using eCommerce.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface IPaymentMethodServices
    {
        Task<IEnumerable<GetPaymentMethod>> GetpaymentMethods();
    }
}
