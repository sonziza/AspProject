using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
