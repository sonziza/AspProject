using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Clients.Base
{
    class BaseClient
    {
        //Адрес контроллера(сервисов), к которому будет обращаться клиент
        protected string Address { get; }
        //http-клиент
        protected HttpClient Http { get; }

        protected BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            Address = ServiceAddress;
            //создаем клиента
            //BaseAddress - адрес, к которому будет обращаться по умолчанию, например, http://localhost:52930
            //DefaultRequestHeaders = в каком формате клиент хочет получать данные (например, json)
            Http = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }
    }
}
