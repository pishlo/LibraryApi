using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Dtos;

namespace ApiProject.Services
{
    public class AuthorService
    {
        private readonly Library_AppContext _context;

        public AuthorService(Library_AppContext context)
        {
            _context = context;
        }

        // Helper method to map Author → AuthorDto
        private static AuthorDto MapToDto(Author author) => new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
            Country = author.Country
        };

        // Get all authors
        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors.Select(MapToDto).ToList();
        }

        // Get author by id
        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return author == null ? null : MapToDto(author);
        }

        // Create author
        public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Author Name is required.");

            var author = new Author
            {
                Name = dto.Name,
                Country = dto.Country
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return MapToDto(author);
        }

        // Update author
        public async Task UpdateAuthorAsync(int id, UpdateAuthorDto dto)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
                throw new KeyNotFoundException("Author not found.");

            existingAuthor.Name = dto.Name;
            existingAuthor.Country = dto.Country;

            await _context.SaveChangesAsync();
        }

        // Delete author
        public async Task DeleteAuthorAsync(int id)
        {
            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
                throw new KeyNotFoundException("Author not found.");

            _context.Authors.Remove(existingAuthor);
            await _context.SaveChangesAsync();
        }
    }
}
