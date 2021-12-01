using System.ComponentModel.DataAnnotations;

namespace ShopBridgeModel
{
    public class Insert_Inventory_Request
    {
        [Required(ErrorMessage = "T_ID is required")]
        public long T_ID { get; set; }
        [Required(ErrorMessage = "SubCat_ID is required")]
        public int SubCat_ID { get; set; }
        [Required(ErrorMessage = "Unit_Price is required")]
        public long Unit_Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Total_Price is required")]
        public decimal Total_Price { get; set; }
        [Required(ErrorMessage = "Product_Description is required")]
        public string Product_Description { get; set; }
        [Required(ErrorMessage = "Created_By is required")]
        public string Created_By { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }
}
