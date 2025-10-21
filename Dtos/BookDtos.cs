namespace ApiProject.Dtos
{
    // Output DTO for Book (includes Author info)
    public class BookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
    }

    // Input DTO for creating a Book
    public class CreateBookDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
    }

    // Input DTO for updating a Book
    public class UpdateBookDto
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
    }
}