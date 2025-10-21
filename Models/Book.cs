using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = null!;

        [StringLength(30, ErrorMessage = "Genre cannot exceed 30 characters.")]
        public string? Genre { get; set; }

        [Range(1000, 9999, ErrorMessage = "Year must be between 1000 and 9999.")]
        public int Year { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}