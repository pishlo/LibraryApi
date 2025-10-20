using ApiProject.Data;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Services
{
    public class BorrowRecordService
    {
        private readonly Library_AppContext _context;

        public BorrowRecordService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowRecord>> GetAllBorrowRecordsAsync()
        {
            return await _context.BorrowRecords
                                 .Include(br => br.Book)
                                 .Include(br => br.Member)
                                 .ToListAsync();
        }

        public async Task<BorrowRecord?> GetBorrowRecordByIdAsync(int id)
        {
            return await _context.BorrowRecords
                                 .Include(br => br.Book)
                                 .Include(br => br.Member)
                                 .FirstOrDefaultAsync(br => br.Id == id);
        }

        public async Task<string> CreateBorrowRecordAsync(BorrowRecord record)
        {
            var bookExists = await _context.Books.AnyAsync(b => b.Id == record.BookId);
            var memberExists = await _context.Members.AnyAsync(m => m.Id == record.MemberId);

            if (!bookExists)
                return "Book not found.";
            if (!memberExists)
                return "Member not found.";

            record.BorrowDate = DateTime.UtcNow;
            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return "Borrow record created successfully.";
        }

        public async Task<bool> ReturnBookAsync(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
                return false;

            record.ReturnDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBorrowRecordAsync(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
                return false;

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
