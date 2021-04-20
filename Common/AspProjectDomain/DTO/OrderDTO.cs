﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspProjectDomain.ViewModel;

namespace AspProjectDomain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; }

        public IList<OrderItemDTO> Items { get; set; }
    }
}