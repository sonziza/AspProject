using AspProjectDomain.Entities.Base;
using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities
{
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        //отношение "один-ко-многим к таблице Products
        public ICollection<Product> Products { get; set; }
    }
}
