using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectDomain.Entities
{
    class Cart
    {
        //коллекция товаров в корзине
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
    //описание товара в корзине
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
