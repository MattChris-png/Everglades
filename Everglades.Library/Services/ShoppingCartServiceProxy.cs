using Everglades.Library.DTO;
using Everglades.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.Library.Services
{
    public class ShoppingCartServiceProxy
    {
        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock  = new object();

        private List<ShoppingCart> carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        public ShoppingCart Cart
        {

            get
            {
                if (!carts.Any())
                {
                   
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;                    
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }
/*
        public ShoppingCart AddOrUpdate(ShoppingCart c)
        {

        }
*/
        public void AddToCart(ProductDTO newProduct)
        {
            if(Cart ==null || Cart.Contents == null)
            {
                return;
            }
            var existingProduct = Cart.Contents?.FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);
            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            
            if(inventoryProduct == null)
            {
                return;
            }

            inventoryProduct.Quantity -= newProduct.Quantity;
            
            
            if (existingProduct != null)
            {
                existingProduct.Quantity += newProduct.Quantity;
            } else 
            {
                Cart?.Contents?.Add(newProduct);
            }
        }
        private ShoppingCartServiceProxy() {
        
            carts = new List<ShoppingCart>();
        
        }
        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                if(instance == null)
                    instance = new ShoppingCartServiceProxy();
                return instance; 
            }
        }
    }
}
