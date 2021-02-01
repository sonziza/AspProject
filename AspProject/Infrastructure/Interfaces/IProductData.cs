using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspProjectDomain.Entities;

namespace AspProject.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
    }
}
