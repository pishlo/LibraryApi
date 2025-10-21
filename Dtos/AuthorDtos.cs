using System.ComponentModel.DataAnnotations;

namespace ApiProject.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    public class CreateAuthorDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? Country { get; set; }
    }

    public class UpdateAuthorDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? Country { get; set; }
    }
}