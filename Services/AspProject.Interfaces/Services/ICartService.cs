using AspProjectDomain.ViewModel;

namespace AspProject.Interfaces.Services
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
