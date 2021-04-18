using Quickkart.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quickkart.Services.Controllers
{
    public class HomeController : ApiController
    {
        public UserBL userObj { get; set; }
        public HomeController()
        {
            userObj = new UserBL();
        }

        public HomeController(UserBL userObj)
        {
            this.userObj = userObj;
        }

        [HttpPost]
        public int Login(Common.Models.User user)
        {
            int res = 0;
            res = userObj.UserLogin(user.EmailId, user.Password);
            return res;
        }

        [HttpPost]
        public int RegisterUser(Common.Models.User modelUser)
        {
            int res = 0;
            res = userObj.RegisterUser(modelUser);
            return res;
        }

    }
}
