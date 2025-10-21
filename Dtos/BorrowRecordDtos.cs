using System;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.Dtos
{
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

    public class CreateBorrowRecordDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int MemberId { get; set; }
    }
}