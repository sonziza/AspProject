using AspProjectDomain.Entities.Base;
using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspProjectDomain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        /// <summary>Родительская секция (при наличии)</summary>
        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        //отношение "один-ко-многим" внутри одной таблицы
        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }

    }

}
