using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practice_test1.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId {  get; set; }
        [Required]
        [StringLength(50)]
        [Column("Title", TypeName = "varchar")]
        public string Title { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Author", TypeName = "varchar")]
        public string Author {  get; set; }
        [Required]
        [StringLength(50)]
        [Column("Genre", TypeName = "varchar")]
        public string Genre {  get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
    }
}
