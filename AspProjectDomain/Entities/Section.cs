using AspProjectDomain.Entities.Base;
using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        /// <summary>Родительская секция (при наличии)</summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }
    }

}
