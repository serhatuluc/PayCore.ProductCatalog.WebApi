
namespace PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto
{
   public class ProductViewDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public double Price { get; set; }
        public bool IsSold { get; set; }
        public bool IsOfferable { get; set; }
        public bool Status { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
