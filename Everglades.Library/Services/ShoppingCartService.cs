using Everglades.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.Library.Services
{
    internal class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock  = new object();

        public ReadOnlyCollection<ShoppingCart> carts;

        public ShoppingCart Cart
        {

            get
            {
                if (carts == null)
                {
                    return new ShoppingCart();                    
                }
                carts.FirstOrDefault();
            }
        }

        private ShoppingCartService() { }
        public static ShoppingCartService Current
        {
            get
            {
                lock(instanceLock)
                if(instance == null)
                    instance = new ShoppingCartService();
                return instance; 
            }
        }
    }
}
