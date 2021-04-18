using Quickkart.App.Authorization;
using Quickkart.Business;
using Quickkart.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace Quickkart.App.Controllers
{
    public class ProductController : Controller
    {
        Repository.IServiceRepository serviceObj;
        public Repository.IServiceRepository ServiceObj { get { return serviceObj; } }
        public ProductController()
        {
            serviceObj = new Repository.ServiceRepository();
        }

        public ProductController(Repository.IServiceRepository repository)
        {
            serviceObj = repository;
        }

        [AdminAuthorize]
        public ActionResult ViewProducts()
        {
            try
            {
                HttpResponseMessage response = ServiceObj.GetResponse("api/Product/ViewProducts");
                response.EnsureSuccessStatusCode();
                var product = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                List<Models.Product> lstProduct = new List<Models.Product>();
                Models.Product prod;
                foreach (var item in product)
                {
                    prod = new Models.Product();
                    prod.ProductId = item.ProductId;
                    prod.ProductName = item.ProductName;
                    prod.CategoryId = item.CategoryId;
                    prod.QuantityAvailable = item.QuantityAvailable;
                    prod.Price = item.Price;
                    lstProduct.Add(prod);
                }
                return View("ViewProducts", lstProduct);
            }
            catch(Exception e)
            {
                ViewBag.Message = "No Products found!";
                return View("Error");
            }
        }

        public ActionResult ViewCart()
        {
            string email = Session["userName"].ToString();
            try
            {
                HttpResponseMessage response = ServiceObj.GetResponse("api/Product/ViewCart?email=" + email);
                response.EnsureSuccessStatusCode();
                var cartRes = response.Content.ReadAsAsync<IEnumerable<Cart>>().Result;
                List<Models.Cart> lstCart = new List<Models.Cart>();
                Models.Cart cart;
                if (cartRes != null)
                {
                    foreach (var item in cartRes)
                    {
                        cart = new Models.Cart();
                        cart.ProductId = item.ProductId;
                        cart.ProductName = item.ProductName;
                        cart.Price = item.Price;
                        cart.QuantityPurchased = item.QuantityPurchased;
                        cart.TotalAmount = item.TotalAmount;
                        lstCart.Add(cart);
                    }
                    return View("ViewCart", lstCart);
                }
                else
                {
                    ViewBag.Message = "No products in your cart. Add products to your cart!";
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "No products in your cart. Add products to your cart!";
                return View("Error");
            }
        }

        public ActionResult AddToCart(Models.Product prod)
        {
            int res;
            try
            {
                Common.Models.Cart cart = new Common.Models.Cart();
                cart.EmailId = Session["userName"].ToString();
                cart.ProductId = prod.ProductId;
                cart.QuantityPurchased = 1;
                HttpResponseMessage response = ServiceObj.LoginResponse("api/Product/AddCart", cart);
                response.EnsureSuccessStatusCode();
                res = response.Content.ReadAsAsync<int>().Result;
                if (res == 1)
                {
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    ViewBag.Message = "Product is not added to Cart. Some problem occured. Please try again!";
                    return View("Error");
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "Product is not added to Cart. Some problem occured. Please try again!";
                return View("Error");
            }
        }


        public ActionResult UpdateCart(Models.Cart cart)
        {
            return View("UpdateCart", cart);
        }

        [HttpPost]
        public ActionResult SaveUpdatedCart(Models.Cart cart)
        {
            int res;
            try
            {
                if (ModelState.IsValid)
                {
                    Common.Models.Cart cartObj = new Common.Models.Cart();
                    cartObj.EmailId = Session["userName"].ToString();
                    cartObj.ProductId = cart.ProductId;
                    cartObj.QuantityPurchased = cart.QuantityPurchased;
                    HttpResponseMessage response = ServiceObj.PutRequest("api/Product/UpdateCart", cartObj);
                    response.EnsureSuccessStatusCode();
                    res = response.Content.ReadAsAsync<int>().Result;
                    if (res == 1)
                    {
                        return RedirectToAction("ViewCart");
                    }
                    else
                    {
                        ViewBag.Message = "Cart is not updated. Some error occured. Please try again!";
                        return View("Error");
                    }
                }
                else
                {
                    return RedirectToAction("UpdateCart", cart);
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Cart is not updated. Some error occured. Please try again!";
                return View("Error");
            }
        }

        public ActionResult DeleteCart(Models.Cart cart)
        {
            return View("DeleteCart", cart);
        }

        [HttpPost]
        public ActionResult DeleteCart(string prodId)
        {
            int res;
            string email = Session["userName"].ToString();
            try
            {
                HttpResponseMessage response = ServiceObj.DeleteRequest("api/Product/DeleteCart?email=" + email + "&prodId=" + prodId);
                response.EnsureSuccessStatusCode();
                res = response.Content.ReadAsAsync<int>().Result;
                if (res == 1)
                {
                    return RedirectToAction("ViewCart");
                }
                else
                {
                    ViewBag.Message = "Products in the cart is not deleted. Try again!";
                    return View("Error");
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "Products in the cart is not deleted. Try again!";
                return View("Error");
            }
        }

        public ActionResult CheckOut()
        {
            return View();
        }


        public ActionResult ForCheckOut()
        {
            string email = Session["userName"].ToString();
            string orderId;
            Models.Cart cart = new Models.Cart();
            cart.EmailId = email;
            try
            {
                HttpResponseMessage response = ServiceObj.RegisterResponse("api/Product/CheckOut", cart);
                response.EnsureSuccessStatusCode();
                orderId = response.Content.ReadAsAsync<string>().Result;
                if (orderId != null)
                {
                    ViewBag.orderID = orderId;
                    return View("OrderSucceed");
                }
                else
                {
                    ViewBag.Message = "Some error occured. Proceed to checkout again!";
                    return View("Error");
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "Some error occured. Proceed to checkout again!";
                return View("Error");
            }
        }

        public ActionResult ViewOrders()
        {
            string email = Session["userName"].ToString();
            try
            {
                HttpResponseMessage response = ServiceObj.GetResponse("api/Product/ViewOrders?emailId=" + email);
                response.EnsureSuccessStatusCode();
                var cart = response.Content.ReadAsAsync<IEnumerable<Cart>>().Result;
                List<Models.Cart> lstOrder = new List<Models.Cart>();
                Models.Cart cartObj;
                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        cartObj = new Models.Cart();
                        cartObj.OrderId = item.OrderId;
                        cartObj.ProductName = item.ProductName;
                        cartObj.QuantityPurchased = item.QuantityPurchased;
                        cartObj.TotalAmount = item.TotalAmount;
                        cartObj.OrderDate = item.OrderDate;
                        lstOrder.Add(cartObj);
                    }
                    return View("ViewOrders", lstOrder);
                }
                else
                {
                    ViewBag.Message = "Some error occured!";
                    return View("Error");
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "You didn't purchase anything. Go to your cart and make your first shopping!";
                return View("Error");
            }
        }

        //public UserBL userObj { get; set; }
        //public ProductController()
        //{
        //    userObj = new UserBL();
        //}

        //public ProductController(UserBL userObj)
        //{
        //    this.userObj = userObj;
        //}
        //// GET: Product
        //[AdminAuthorize]
        //public ActionResult ViewProducts()
        //{
        //    var product = userObj.ViewProduct();
        //    List<Models.Product> lstProduct = new List<Models.Product>();
        //    Models.Product prod;
        //    if (product != null && product.Count != 0)
        //    {
        //        foreach (var item in product)
        //        {
        //            prod = new Models.Product();
        //            prod.ProductId = item.ProductId;
        //            prod.ProductName = item.ProductName;
        //            prod.CategoryId = item.CategoryId;
        //            prod.QuantityAvailable = item.QuantityAvailable;
        //            prod.Price = item.Price;
        //            lstProduct.Add(prod);
        //        }
        //        return View("ViewProducts", lstProduct);
        //    }
        //    else
        //    {
        //        ViewBag.Message = "No products found";
        //        return View("Error");
        //    }
        //}

        //[AdminAuthorize]
        //public ActionResult ViewCart()
        //{
        //    string email = Session["userName"].ToString();
        //    List<Models.Cart> lstCart = new List<Models.Cart>();
        //    var cartRes = userObj.ViewCart(email);
        //    Models.Cart cart;
        //    Console.WriteLine(cartRes);
        //    if (cartRes != null && cartRes.Count!=0)
        //    {
        //        foreach (var item in cartRes)
        //        {
        //            cart = new Models.Cart();
        //            cart.ProductId = item.ProductId;
        //            cart.ProductName = item.ProductName;
        //            cart.Price = item.Price;
        //            cart.QuantityPurchased = item.QuantityPurchased;
        //            cart.TotalAmount = item.TotalAmount;
        //            lstCart.Add(cart);
        //        }
        //        return View("ViewCart", lstCart);
        //    }
        //    else
        //    {
        //        ViewBag.Message = "No products in your cart. Add products to your cart!";
        //        return View("Error");
        //    }
        //}

        //public ActionResult AddToCart(Models.Product prod)
        //{
        //    Common.Models.Cart cart = new Common.Models.Cart();
        //    cart.EmailId = Session["userName"].ToString();
        //    cart.ProductId = prod.ProductId;
        //    cart.QuantityPurchased = 1;
        //    int result = userObj.AddToCart(cart);
        //    if(result == 1)
        //    {
        //        return RedirectToAction("ViewCart");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Product is not added to Cart. Some problem occured. Please try again!";
        //        return View("Error");
        //    }
        //}

        //public ActionResult UpdateCart(Models.Cart cart)
        //{
        //    return View("UpdateCart", cart);
        //}
        //public ActionResult SaveUpdatedCart(Models.Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Common.Models.Cart cartObj = new Common.Models.Cart();
        //        cartObj.EmailId = Session["userName"].ToString();
        //        cartObj.ProductId = cart.ProductId;
        //        cartObj.QuantityPurchased = cart.QuantityPurchased;
        //        int result = userObj.UpdateCart(cartObj);
        //        if (result == 1)
        //        {
        //            return RedirectToAction("ViewCart");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Cart is not updated. Some error occured. Please try again!";
        //            return View("Error");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("UpdateCart", cart);
        //    }
        //}

        //public ActionResult DeleteCart(Models.Cart cart)
        //{
        //    return View("DeleteCart", cart);
        //}

        //[HttpPost]
        //public ActionResult DeleteCart(string prodId)
        //{
        //    string email = Session["userName"].ToString();
        //    int result = userObj.DeleteCart(email, prodId);
        //    if(result == 1)
        //    {
        //        return RedirectToAction("ViewCart");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Products in the cart is not deleted. Try again!";
        //        return View("Error");
        //    }
        //}

        //public ActionResult CheckOut()
        //{
        //    return View();
        //}

        //public ActionResult ForCheckOut()
        //{
        //    string email = Session["userName"].ToString();
        //    string orderId = userObj.Order(email);
        //    if (orderId != null)
        //    {
        //        ViewBag.orderID = orderId;
        //        return View("OrderSucceed");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Some error occured. Proceed to checkout again!";
        //        return View("Error");
        //    }
        //}

        //[AdminAuthorize]
        //public ActionResult ViewOrders()
        //{
        //    string email = Session["userName"].ToString();
        //    var cart = userObj.ViewOrders(email);
        //    List<Models.Cart> lstOrder = new List<Models.Cart>();
        //    Models.Cart cartObj;
        //    if (cart != null && cart.Count!=0)
        //    {
        //        foreach (var item in cart)
        //        {
        //            cartObj = new Models.Cart();
        //            cartObj.OrderId = item.OrderId;
        //            cartObj.ProductName = item.ProductName;
        //            cartObj.QuantityPurchased = item.QuantityPurchased;
        //            cartObj.TotalAmount = item.TotalAmount;
        //            cartObj.OrderDate = item.OrderDate;
        //            lstOrder.Add(cartObj);
        //        }
        //        return View("ViewOrders", lstOrder);
        //    }
        //    else
        //    {
        //        ViewBag.Message = "You didn't purchase anything. Go to your cart and make your first shopping!";
        //        return View("Error");
        //    }
        //}
    }
}