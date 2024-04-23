using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WMG.DVDCentral.BL.Models;

namespace WMG.DVDCentral.BL
{
    public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Movie movie)
        {
            if (cart != null) { cart.Items.Add(movie); }
        }

        public static void Remove(ShoppingCart cart, Movie movie)
        {
            if (cart != null) { cart.Items.Remove(movie); }
        }

        public static void Checkout(ShoppingCart cart, User user)
        {
            Customer customer = CustomerManager.LoadById(user.CustomerId);
            // Make a new order
            Order order = new Order
            {
                // Set the Order Fields as needed

                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now.AddDays(4),
                CustomerId = customer.Id,
                UserId = user.Id
            };


            // foreach (Movie item in cart.items)
            foreach (Movie item in cart.Items)
            {
                // Make a new orderitem
                OrderItem orderItem = new OrderItem
                {
                    // Set the OrderItem fields from the item.
                    // OrderId = order.Id,
                    Quantity = item.InStkQty,
                    MovieId = item.Id,
                    Cost = item.Cost,
                };
                // order.OrderItems.Add(orderItem)
                order.OrderItems.Add(orderItem);

            }

            // OrderManager.Insert(order)
            OrderManager.Insert(order);

            if (order.OrderItems.Count() > 0)
            {
                // Decrement the tblMovie.InStkQty appropriately
                foreach (Movie item in cart.Items)
                {
                    Movie movie = MovieManager.LoadById(item.Id);
                    movie.InStkQty = movie.InStkQty - 1;
                    MovieManager.Update(movie);
                }
            }

            // Make a new empty shopping cart once finished
            cart = new ShoppingCart();
        }




    }
}
