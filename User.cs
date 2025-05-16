using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApiProject.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}