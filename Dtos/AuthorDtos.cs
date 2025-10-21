namespace ApiProject.Dtos
{
    // Output DTO (for GET requests)
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    // Input DTO for creating a new author
    public class CreateAuthorDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    // Input DTO for updating an author
    public class UpdateAuthorDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
    }
}