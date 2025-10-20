using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Models;

namespace ApiProject.Services
{
    public class BookService
    {
        private readonly Library_AppContext _context;

        public BookService(Library_AppContext context)
        {
            _context = context;
        }

        // Get all books (including authors)
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .ToListAsync();
        }

        // Get book by id
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Create book
        public async Task<Book> CreateBookAsync(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Book Title is required.");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
                throw new ArgumentException("AuthorId does not exist.");

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        // Update book
        public async Task UpdateBookAsync(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
                throw new ArgumentException("Id mismatch.");

            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                throw new KeyNotFoundException("Book not found.");

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == updatedBook.AuthorId);
            if (!authorExists)
                throw new ArgumentException("AuthorId does not exist.");

            existingBook.Title = updatedBook.Title;
            existingBook.Genre = updatedBook.Genre;
            existingBook.Year = updatedBook.Year;
            existingBook.AuthorId = updatedBook.AuthorId;

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
