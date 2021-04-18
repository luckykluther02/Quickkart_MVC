using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quickkart.App.Repository
{
    public interface IServiceRepository
    {
        HttpResponseMessage GetResponse(string url);
        HttpResponseMessage LoginResponse(string url, object model);
        HttpResponseMessage RegisterResponse(string url, object model);
        HttpResponseMessage PutRequest(string url, object model);
        HttpResponseMessage DeleteRequest(string url);


    }
}
