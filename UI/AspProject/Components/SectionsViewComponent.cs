using AspProject.Infrastructure.Interfaces;
using AspProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke()
        {
            //собираем инфо по секциям из ProductData
            var sections = _ProductData.GetSections();

            //собираем инфо по радительским секциям из ProductData
            var parent_sections = sections.Where(s => s.ParentId is null);

            //преобразуем их в модель представления ViewModel (то, что будет отправлено пользователю)
            var parent_sections_views = parent_sections
               .Select(s => new SectionViewModel
               {
                   Id = s.Id,
                   Name = s.Name,
                   Order = s.Order
               })
               .ToList();
            //отсортируем списки 1
            int OrderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);
            //создаем дерево каталогов (для каждой родительской секции находим дочернюю по ParentId) 
            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in childs)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section
                    });

                parent_section.ChildSections.Sort(OrderSortMethod);
            }
            //отсортируем списки 2
            parent_sections_views.Sort(OrderSortMethod);

            return View(parent_sections_views);
        } 
    }
}
