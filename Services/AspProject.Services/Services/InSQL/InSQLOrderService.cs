﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProject.DAL.Context;
using AspProject.Interfaces.Services;
using AspProject.Services.Mapping;
using AspProjectDomain.DTO;
using AspProjectDomain.Entities;
using AspProjectDomain.Entities.Identity;
using AspProjectDomain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspProject.Services.Services.InSQL
{
    [Authorize]
    public class InSQLOrderService : IOrderService
    {
        private readonly AspProjectDbContext _db;
        private readonly UserManager<User> _UserManager;

        public InSQLOrderService(AspProjectDbContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync())
            .Select(order => order.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id)).ToDTO();


        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь с именем {UserName} в БД отсутствует");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user
            };

            //var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            //var cart_products = await _db.Products
            //  .Where(p => product_ids.Contains(p.Id))
            //  .ToArrayAsync();

            //order.Items = Cart.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,  // место, где могут быть применены скидки
            //        Quantity = cart_item.Quantity,
            //    }).ToArray();

            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.ToDTO();
        }
    }
}
