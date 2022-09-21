

using System.Collections.Generic;

namespace PayCore.ProductCatalog.Domain.Entities
{
    public class Brand:BaseEntity
    {
        public virtual string BrandName { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
