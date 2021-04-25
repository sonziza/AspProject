using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspProject.Interfaces;
using AspProject.Interfaces.Services;
using AspProjectDomain.DTO;

namespace AspProject.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersAPIController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersAPIController(IOrderService OrderService) => _OrderService = OrderService;

        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await _OrderService.GetUserOrders(UserName);

        [HttpGet("{id:int}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _OrderService.GetOrderById(id);

        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) =>
            await _OrderService.CreateOrder(UserName, OrderModel);
    }
}
