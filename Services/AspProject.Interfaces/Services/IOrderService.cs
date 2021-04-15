using System.Collections.Generic;
using System.Threading.Tasks;
using AspProjectDomain.Entities;
using AspProjectDomain.ViewModel;

namespace AspProject.Interfaces.Services
{
    public interface IOrderService
    {

        Task<IEnumerable<Order>> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
