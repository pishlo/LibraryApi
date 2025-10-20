using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Services;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            try
            {
                var createdAuthor = await _authorService.CreateAuthorAsync(author);
                return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(id, author);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
