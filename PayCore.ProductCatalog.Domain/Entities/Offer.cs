

namespace PayCore.ProductCatalog.Domain.Entities
{
    public class Offer:BaseEntity
    {
        public virtual bool IsApproved { get; set; } 
        public virtual int OfferedPrice { get; set; }
        public virtual bool Status { get; set; } = true;
        public virtual Product Product { get; set; }
        public virtual Account Customer { get; set; }
    }
}
