using AspProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void Add(int id);
        //уменьшать количество товара
        void Decrement(int id);
        //удалять товар целиком из корзины
        void Remove(int id);
        //очищаться по завершению задач
        void Clear();
        //преобразование содержимого корзины во ViewModel
        CartViewModel GetViewModel();
    }
}
