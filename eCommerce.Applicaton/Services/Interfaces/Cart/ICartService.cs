﻿using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckOutHistory(IEnumerable<CreateArchive> newArchive);
        Task<ServiceResponse> CheckOutHistory(Checkout checkouts);
    }
}
