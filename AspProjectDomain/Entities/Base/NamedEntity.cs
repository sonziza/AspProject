using AspProjectDomain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities.Base
{
    public class NamedEntity : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
