// Books.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Title { get; set; }

        [StringLength(30)]
        public string? Genre { get; set; }

        [Range(1000, 9999, ErrorMessage = "Year must be between 1000 and 9999.")]
        public int Year { get; set; }

        // Foreign key for Author
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        // Navigation property
        public Author? Author { get; set; }
    }
}