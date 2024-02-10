using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_test1.Entities

{
    [Table("Users")]
    public class User
    {
        [Key]

        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Username", TypeName = "varchar")]
        public string? UserName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Email", TypeName = "varchar")]
        public string? Email { get; set; }
        
        
        
        [Required]
        [StringLength(20)]
        [Column("Password", TypeName = "varchar")]
        public string? Password { get; set; }

       public string? RoleName { get; set; }

    }
}
