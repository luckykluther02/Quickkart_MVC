using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Quickkart.App.Repository
{
    public class ServiceRepository: IServiceRepository
    {
        private HttpClient Client { get; set; }

        public ServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
        }

        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }

        public HttpResponseMessage LoginResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage RegisterResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }


        public HttpResponseMessage PutRequest(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage DeleteRequest (string url)
        {
            return Client.DeleteAsync(url).Result;
        }

    }
}