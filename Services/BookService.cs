using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;
using ApiProject.Dtos;

namespace ApiProject.Services
{
    public class BookService
    {
        private readonly Library_AppContext _context;

        public BookService(Library_AppContext context)
        {
            _context = context;
        }

        // Helper method to map Book → BookDto
        private static BookDto MapToDto(Book book) => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Genre = book.Genre,
            Year = book.Year,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name
        };

        // Get all books (including authors)
        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            return books.Select(MapToDto).ToList();
        }

        // Get book by id
        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.Include(b => b.Author)
                                           .FirstOrDefaultAsync(b => b.Id == id);
            return book == null ? null : MapToDto(book);
        }

        // Create book
        public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Book Title is required.");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            if (!authorExists)
                throw new ArgumentException("AuthorId does not exist.");

            var book = new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Year = dto.Year,
                AuthorId = dto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Load Author for DTO mapping
            await _context.Entry(book).Reference(b => b.Author).LoadAsync();

            return MapToDto(book);
        }

        // Update book
        public async Task UpdateBookAsync(int id, UpdateBookDto dto)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException("Book not found.");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            if (!authorExists)
                throw new ArgumentException("AuthorId does not exist.");

            existingBook.Title = dto.Title;
            existingBook.Genre = dto.Genre;
            existingBook.Year = dto.Year;
            existingBook.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
        }

        // Delete book
        public async Task DeleteBookAsync(int id)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException("Book not found.");

            _context.Books.Remove(existingBook);
            await _context.SaveChangesAsync();
        }
    }
}
