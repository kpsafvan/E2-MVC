using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E2.Models
{
    public class LocationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LocationId { get; set; }
        public string Name { get; set; }

        public virtual List<StockModel> Stocks { get; set; }
    }
}
