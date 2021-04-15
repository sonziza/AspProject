using AspProject.Infrastructure.Interfaces;
using AspProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService) => _CartService = CartService;

        public IActionResult Index() => View(new CartOrderViewModel { Cart = _CartService.GetViewModel() });

        public IActionResult Add(int id)
        {
            _CartService.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _CartService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _CartService.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _CartService.Clear();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Checkout(OrderViewModel orderViewModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _CartService.GetViewModel(),
                    Order = orderViewModel,
                });

            var order = await orderService.CreateOrder(
                User.Identity!.Name,
                _CartService.GetViewModel(),
                orderViewModel
                );

            _CartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id });
        }
        public async Task<IActionResult> OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
