namespace AutoParts.Web.Client.Public.Cart.Services
{
    using System.Collections.Generic;

    using Blazored.LocalStorage;

    using Protos;

    using Models;

    using Shared.Constants;
    using System.Linq;
    using System;

    public class CartService
    {
        private readonly ISyncLocalStorageService localStorage;
        private List<CartItemModel> cartItems;
        private List<Action> listeners;

        public CartService(ISyncLocalStorageService localStorage)
        {
            Console.WriteLine("INITIALIZED!");
            this.localStorage = localStorage;
            cartItems = ReadCartItemsFromLocalStorage();
            listeners = new List<Action>();
        }

        public CartItemModel GetAutoPart(long autoPartId)
        {
            return cartItems.FirstOrDefault(cartItem => cartItem.AutoPartId == autoPartId);
        }

        public CartItemModel[] GetAutoParts()
        {
            return cartItems.ToArray();
        }

        public void AddAutoPart(AutoPart autoPart, int quantity)
        {
            var existingCartItem = cartItems.FirstOrDefault(cartItem => cartItem.AutoPartId == autoPart.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = quantity;
            }
            else
            {
                var newCartItem = new CartItemModel
                {
                    AutoPartId = autoPart.Id,
                    Quantity = quantity
                };

                cartItems.Add(newCartItem);
            }

            WriteCartItemsToLocalStorage();
        }

        public void RemoveAutoPart(long autoPartId)
        {
            cartItems = cartItems.Where(cartItems => cartItems.AutoPartId != autoPartId)
                .ToList();

            WriteCartItemsToLocalStorage();
        }

        public void Clear()
        {
            cartItems = new List<CartItemModel>();

            listeners.ForEach(action => action());

            localStorage.RemoveItem(LocalStorageConstants.CartItems);
        }

        public void AddListener(Action action)
        {
            listeners.Add(action);
        }

        private void WriteCartItemsToLocalStorage()
        {
            listeners.ForEach(action => action());

            localStorage.SetItem(LocalStorageConstants.CartItems, cartItems);
        }

        private List<CartItemModel> ReadCartItemsFromLocalStorage()
        {
            if (localStorage.ContainKey(LocalStorageConstants.CartItems))
            {
                return localStorage.GetItem<List<CartItemModel>>(LocalStorageConstants.CartItems);
            }

            return new List<CartItemModel>();
        }
    }
}
