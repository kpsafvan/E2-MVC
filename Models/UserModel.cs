using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace E2.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string UserName { get; set; }
        [Column(TypeName = "VARCHAR(25)")]
        public string  Role { get; set; }

    }
}
