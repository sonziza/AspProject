﻿using AspProject.DAL.Context;
using AspProject.Infrastructure.Interfaces;
using AspProject.ViewModel;
using AspProjectDomain.Entities;
using AspProjectDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Infrastructure.Services.InSQL
{
    public class InSQLOrderService : IOrderService
    {
        private readonly AspProjectDbContext _db;
        private readonly UserManager<User> _UserManager;

        public InSQLOrderService(AspProjectDbContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public async Task<IEnumerable<Order>> GetUserOrders(string UserName) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync();

        public async Task<Order> GetOrderById(int id) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id);


        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден в БД");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.Name,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                User = user,
            };

            var product_ids = Cart.Items.Select(item => item.Product.Id).ToArray();

            var cart_products = await _db.Products
               .Where(p => product_ids.Contains(p.Id))
               .ToArrayAsync();

            order.Items = Cart.Items.Join(
                cart_products,
                cart_item => cart_item.Product.Id,
                product => product.Id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price,  // здесь можно применить скидки к цене товара в заказе
                    Quantity = cart_item.Quantity,
                }).ToArray();

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
    }
}
