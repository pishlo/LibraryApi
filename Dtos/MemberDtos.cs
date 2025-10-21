namespace ApiProject.Dtos
{
    // Output DTO
    public class MemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }

    // Input DTO for creating a Member
    public class CreateMemberDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }

    // Input DTO for updating a Member
    public class UpdateMemberDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}