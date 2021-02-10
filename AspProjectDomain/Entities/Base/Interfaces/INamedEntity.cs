using System;
using System.Collections.Generic;
using System.Text;

namespace AspProjectDomain.Entities.Base.Interfaces
{
    public interface INamedEntity : IBaseEntity
    {
        string Name { get; set; }
    }
}
