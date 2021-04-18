using Quickkart.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Quickkart.App.Controllers
{
    public class HomeController : Controller
    {
        Repository.IServiceRepository serviceObj;
        public Repository.IServiceRepository ServiceObj { get { return serviceObj; } }
        public HomeController()
        {
            serviceObj = new Repository.ServiceRepository();
        }

        public HomeController(Repository.IServiceRepository repository)
        {
            serviceObj = repository;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            int res;
            Models.User user = new Models.User();
            user.EmailId = frm[0];
            user.Password = frm[1];
            try
            {
                HttpResponseMessage response = ServiceObj.LoginResponse("api/Home/Login", user);
                response.EnsureSuccessStatusCode();
                res = response.Content.ReadAsAsync<int>().Result;
                if (res == 1 || res == 2)
                {
                    this.Session["userName"] = frm[0];
                    this.Session["role"] = res;
                    HttpCookie cookieObj = new HttpCookie("User");
                    cookieObj.Values.Add("userId", frm[0]);
                    cookieObj.Values.Add("pwd", frm[1]);
                    cookieObj.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookieObj);
                    return View("MyHome");
                }
                else
                {
                    return View("Login");

                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "Login failed. Please try again!";
                return View("Error");
            }
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(Models.User modelUser)
        {
            int res;
            if (ModelState.IsValid)
            {
                Common.Models.User user = new Common.Models.User();
                user.EmailId = modelUser.EmailId;
                user.FirstName = modelUser.FirstName;
                user.LastName = modelUser.LastName;
                user.Password = modelUser.Password;
                user.Gender = modelUser.Gender;
                user.Mobile = modelUser.Mobile;
                user.DateOfBirth = modelUser.DateOfBirth;
                user.Address = modelUser.Address;

                try
                {
                    HttpResponseMessage response = ServiceObj.RegisterResponse("api/Home/RegisterUser", user);
                    response.EnsureSuccessStatusCode();
                    res = response.Content.ReadAsAsync<int>().Result;
                    if (res == 1)
                    {
                        ViewBag.Message = "Registration successful!";
                        return View("Success");
                    }
                    else
                    {
                        ViewBag.Message = "Register failed. Try again!";
                        return View("Error");
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Message = "Register failed. Try again!";
                    return View("Error");
                }         
            }
            else
            {
                return View("RegisterUser", modelUser);
            }
        }

        public ActionResult LogOut()
        {
            Session.Remove("userName");
            return View("Index");
        }
        public ActionResult MyHome()
        {
            return View();
        }

    }
}