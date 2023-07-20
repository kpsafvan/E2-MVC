using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E2.Models
{
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId{ get; set; }
        [Column(TypeName="Varchar(50)")]
        public string Name { get; set; }
        public long Parent { get; set; }//Doubt in Validation
        public bool isDeleted { get; set; }
        public long CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }
        public long LastModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime LastModifiedDate { get; set; }
        //FK One category many products
        public virtual List<ProductModel>? Product { get; set; }
        
    }
}
