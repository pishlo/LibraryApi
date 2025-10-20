using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;

namespace ApiProject.Services
{
    public class AuthorService
    {
        private readonly Library_AppContext _context;

        public AuthorService(Library_AppContext context)
        {
            _context = context;
        }

        // Get all authors
        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        // Get author by id
        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        // Create author
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Author Name is required.");

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }

        // Update author
        public async Task UpdateAuthorAsync(int id, Author updatedAuthor)
        {
            if (id != updatedAuthor.Id)
                throw new ArgumentException("Id mismatch.");

            var existingAuthor = await _context.Authors.FindAsync(id);
            if (existingAuthor == null)
                throw new KeyNotFoundException("Author not found.");

            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.Country = updatedAuthor.Country;

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