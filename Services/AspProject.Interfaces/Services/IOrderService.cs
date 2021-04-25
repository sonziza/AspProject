using System.Collections.Generic;
using System.Threading.Tasks;
using AspProjectDomain.DTO;
using AspProjectDomain.Entities;
using AspProjectDomain.ViewModel;

namespace AspProject.Interfaces.Services
{
    public interface IOrderService
    {

        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel);
    }
}
