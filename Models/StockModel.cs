using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E2.Models
{
    public class StockModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockId { get; set; }
        public long  Quantity  { get; set; }
        public  long LocationId { get; set; }
        public bool isDeleted { get; set; }
        public long CreatedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }
        public long LastModifiedBy { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime LastModifiedDate { get; set; }
        public virtual ProductModel? Product  { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationModel? Location { get; set; }
        public long? ProductId { get; set; }
    }
}
