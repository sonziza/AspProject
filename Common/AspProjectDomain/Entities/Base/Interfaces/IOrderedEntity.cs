using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities.Base.Interfaces
{
    public interface IOrderedEntity : INamedEntity
    {
        int Order { get; set; }
    }
}
