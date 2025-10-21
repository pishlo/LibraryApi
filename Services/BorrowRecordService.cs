using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Dtos;

namespace ApiProject.Services
{
    public class BorrowRecordService
    {
        private readonly Library_AppContext _context;

        public BorrowRecordService(Library_AppContext context)
        {
            _context = context;
        }

        private static BorrowRecordDto MapToDto(BorrowRecord br) => new BorrowRecordDto
        {
            Id = br.Id,
            BookId = br.BookId,
            BookTitle = br.Book?.Title ?? "",
            MemberId = br.MemberId,
            MemberName = br.Member?.Name ?? "",
            BorrowDate = br.BorrowDate,
            ReturnDate = br.ReturnDate
        };

        public async Task<List<BorrowRecordDto>> GetAllBorrowRecordsAsync()
        {
            var records = await _context.BorrowRecords
                                        .Include(br => br.Book)
                                        .Include(br => br.Member)
                                        .ToListAsync();
            return records.Select(MapToDto).ToList();
        }

        public async Task<BorrowRecordDto?> GetBorrowRecordByIdAsync(int id)
        {
            var record = await _context.BorrowRecords
                                       .Include(br => br.Book)
                                       .Include(br => br.Member)
                                       .FirstOrDefaultAsync(br => br.Id == id);
            return record == null ? null : MapToDto(record);
        }

        public async Task<string> CreateBorrowRecordAsync(CreateBorrowRecordDto dto)
        {
            var book = await _context.Books.FindAsync(dto.BookId);
            var member = await _context.Members.FindAsync(dto.MemberId);

            if (book == null) return "Book not found.";
            if (member == null) return "Member not found.";

            var record = new BorrowRecord
            {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                BorrowDate = DateTime.UtcNow
            };

            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return "Borrow record created successfully.";
        }

        public async Task<bool> ReturnBookAsync(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null) return false;

            record.ReturnDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBorrowRecordAsync(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null) return false;

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
