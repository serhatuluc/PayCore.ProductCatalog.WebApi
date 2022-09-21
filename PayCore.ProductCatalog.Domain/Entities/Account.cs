using System;
using System.Collections.Generic;

namespace PayCore.ProductCatalog.Domain.Entities
{
    public class Account :BaseEntity
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Role { get; set; } = "User";
        public virtual DateTime LastActivity { get; set; } = DateTime.Now;
        public virtual IList<Product> Products { get; set; }
        public virtual IList<Offer> Offers { get; set; }

    }
}
