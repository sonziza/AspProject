using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain.DTO;
using Clients.Base;
using Microsoft.Extensions.Configuration;

namespace Clients.Orders
{
    public class OrdersClient:BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, WebAPI.Orders) { }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}");

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var response = await PostAsync($"{Address}/{UserName}", OrderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }
    }
}
