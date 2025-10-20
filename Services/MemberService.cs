using ApiProject.Data;
using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Services
{
    public class MemberService
    {
        private readonly Library_AppContext _context;

        public MemberService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members
                .Include(m => m.BorrowRecords)
                .ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _context.Members
                .Include(m => m.BorrowRecords)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Member> CreateMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<bool> UpdateMemberAsync(Member member)
        {
            var existing = await _context.Members.FindAsync(member.Id);
            if (existing == null)
                return false;

            existing.Name = member.Name;
            existing.Email = member.Email;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return false;

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}