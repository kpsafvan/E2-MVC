using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace E2.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(25)")]
        public string Name { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string Brand { get; set; }
        [Column("Make", TypeName = "VARCHAR(25)")]
        //[Column()]
        public string Made { get; set; }
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Description { get; set; }
        [DisplayName("ManufactureDate")]
        [DataType(DataType.Date)]
        [Column("ManufactureDate", TypeName = "Date")]
        //[Column("ManufactureDate")]
        public System.Nullable<DateTime> ManDate { get; set; }
        [DisplayName("ExpiryDate")]
        [DataType(DataType.Date)]
        [Column("ExpiryDate", TypeName = "Date")]
        public System.Nullable<DateTime> ExpDate { get; set; }
        public long Category { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }
        public long LastModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime LastModifiedDate { get; set; }
        //FK One product many category
        [ForeignKey("CategoryId")]
        public virtual CategoryModel? Cat { get; set; }
        public long? CategoryId { get; set; }
        public virtual List<StockModel>? Stock { get; set; }
    }

}
