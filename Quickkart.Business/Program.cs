using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.Business
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.Models.User user = new Common.Models.User();
            UserBL obj = new UserBL();

            //int res = obj.RegisterUser(user);
            //Console.WriteLine(res);

            //obj.UserLogin(user);

            //List<Common.Models.Cart> listcart = new List<Common.Models.Cart>();
            //listcart = obj.ViewCart("John078@gmail.com");
            //foreach (Common.Models.Cart item in listcart)
            //{
            //    Console.WriteLine(item.ProductName + " " + item.Price + " " + item.QuantityPurchased + " " + item.TotalAmount);
            //}

            //List<Common.Models.Product> listproduct = new List<Common.Models.Product>();
            //listproduct = obj.ViewProduct();
            //foreach (Common.Models.Product item in listproduct)
            //{
            //    Console.WriteLine(item.ProductId + " " + item.ProductName + " " + item.CategoryId + " " + item.Price + " " + item.QuantityAvailable);
            //}

            //Common.Models.Cart cart = new Common.Models.Cart();
            //int res = obj.AddToCart(cart);
            //Console.WriteLine(res);

            //Common.Models.Cart cart = new Common.Models.Cart();
            //int res = obj.UpdateCart(cart);
            //Console.WriteLine(res);

            //Common.Models.Cart cart = new Common.Models.Cart();
            //int res = obj.DeleteCart(cart);
            //Console.WriteLine(res);

            //Common.Models.Cart cart = new Common.Models.Cart();
            //string OrderId = obj.Order(cart);
            //Console.WriteLine(OrderId);

            //List<Common.Models.ViewCart> listorder = new List<Common.Models.ViewCart>();
            //listorder = obj.ViewOrders(user);
            //foreach (Common.Models.ViewCart item in listorder)
            //{
            //    Console.WriteLine(item.ProductName + " " + item.QuantityPurchased + " " + item.TotalAmount + " " + item.OrderId + " " + item.OrderDate);
            //}
        }
    }
}
