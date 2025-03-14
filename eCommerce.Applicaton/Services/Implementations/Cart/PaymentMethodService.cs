﻿using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodServices
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetpaymentMethods()
        {
            var methods = await paymentMethod.GetAllPaymentMethods();
            if(!methods.Any())
                return [];

            return mapper.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
