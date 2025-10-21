using System;

namespace ApiProject.Dtos
{
    // Output DTO
    public class BorrowRecordDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    // Input DTO for creating a BorrowRecord
    public class CreateBorrowRecordDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }
}