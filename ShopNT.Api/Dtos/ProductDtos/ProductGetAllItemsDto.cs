namespace ShopNT.Api.Dtos.ProductDtos
{
    public class ProductGetAllItemsDto
    {
        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal CostPrice { get; set; }

        public string ImageName { get; set; }

        public BrandInProductDto brandinproduct { get; set; }
    }
    
}
