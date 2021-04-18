using Quickkart.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quickkart.Services.Controllers
{
    public class ProductController : ApiController
    {
        public UserBL userObj { get; set; }
        public ProductController()
        {
            userObj = new UserBL();
        }

        public ProductController(UserBL userObj)
        {
            this.userObj = userObj;
        }

        [HttpGet]
        public List<Common.Models.Product> ViewProducts()
        {
            List<Common.Models.Product> lstProduct = new List<Common.Models.Product>();
            lstProduct = userObj.ViewProduct();
            if (lstProduct == null || lstProduct.Count == 0)
            {
                lstProduct = null;
            }
            return lstProduct;
        }

        [HttpGet]
        public List<Common.Models.Cart> ViewCart(string email)
        {
            List<Common.Models.Cart> lstCart = new List<Common.Models.Cart>();
            lstCart = userObj.ViewCart(email);
            if (lstCart == null || lstCart.Count == 0)
            {
                lstCart = null;
            }
            return lstCart;
        }

        public int AddCart(Common.Models.Cart cart)
        {
            int res = userObj.AddToCart(cart);
            return res;
        }

        [HttpPut]
        public int UpdateCart(Common.Models.Cart cart)
        {
            int res = userObj.UpdateCart(cart);
            return res;
        }

        [HttpDelete]
        public int DeleteCart(string email, string prodId)
        {
            int res = userObj.DeleteCart(email, prodId);
            return res;
        }

        public string CheckOut(Common.Models.Cart cart)
        {
            string orderId = userObj.Order(cart.EmailId);
            return orderId;
        }

        [HttpGet]
        public List<Common.Models.Cart> ViewOrders(string emailId)
        {
            List<Common.Models.Cart> lstOrder = new List<Common.Models.Cart>();
            lstOrder = userObj.ViewOrders(emailId);
            if (lstOrder == null || lstOrder.Count == 0)
            {
                lstOrder = null;
            }
            return lstOrder;
        }
    }
}
