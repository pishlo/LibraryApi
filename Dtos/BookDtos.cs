using System.ComponentModel.DataAnnotations;

namespace ApiProject.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }

    public class CreateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(30)]
        public string? Genre { get; set; }

        [Range(1000, 9999)]
        public int Year { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }

    public class UpdateBookDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(30)]
        public string? Genre { get; set; }

        [Range(1000, 9999)]
        public int Year { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}