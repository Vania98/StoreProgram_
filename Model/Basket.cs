using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreProgram_.Model
{
    public class Basket
    {
        [Key]
        public int BasketID { get; set; }

        [Required]
        public int ClientID { get; set; } 

        [Required]
        [MaxLength(15)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [ForeignKey("ClientID")]
        public Client? Client { get; set; }
    }
}
