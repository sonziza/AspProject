using AspProjectDomain.Entities.Base;
using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjectDomain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        //отношение "один-ко-многим"
        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        //т.к. у нас в ходе проектирования уже используется св-во 
        //BrandId и тп, то используем два свойства
        public int? BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        public string ImageUrl { get; set; }
        //!!!для БД важно указать точность!!!
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
