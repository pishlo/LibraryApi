using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Dtos;

namespace ApiProject.Services
{
    public class MemberService
    {
        private readonly Library_AppContext _context;

        public MemberService(Library_AppContext context)
        {
            _context = context;
        }

        // Map Member → MemberDto
        private static MemberDto MapToDto(Member member) => new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber
        };

        public async Task<List<MemberDto>> GetAllMembersAsync()
        {
            var members = await _context.Members.ToListAsync();
            return members.Select(MapToDto).ToList();
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            return member == null ? null : MapToDto(member);
        }

        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto dto)
        {
            var member = new Member
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return MapToDto(member);
        }

        public async Task<bool> UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var existing = await _context.Members.FindAsync(id);
            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;

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
