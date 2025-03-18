﻿using eCommerce.Domain.Entities.Cart;

namespace eCommerce.Domain.Interfaces.Cart
{
    public interface ICart
    {
        Task<int> SaveCheckOutHistory(IEnumerable<Archive> checkouts);
    }
}
