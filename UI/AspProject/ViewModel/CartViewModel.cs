using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.ViewModel
{
    public class CartViewModel
    {
        //перечисление кортежей (Продукт, его количество)
        //Enumerable необходим для его манипуляций
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }
        //всего товаров внутри корзины
        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
        //полная стоимость корзины
        public decimal TotalPrice => Items?.Sum(item => item.Product.Price * item.Quantity) ?? 0m;
    }
}
