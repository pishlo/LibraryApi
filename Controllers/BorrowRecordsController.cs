using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Services;

namespace ApiProject.Controllers
{
    [Route("api/borrowrecords")]
    [ApiController]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly BorrowRecordService _borrowService;

        public BorrowRecordsController(BorrowRecordService borrowService)
        {
            _borrowService = borrowService;
        }

        // GET: api/borrowrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            var records = await _borrowService.GetAllBorrowRecordsAsync();
            return Ok(records);
        }

        // GET: api/borrowrecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecord>> GetBorrowRecord(int id)
        {
            var record = await _borrowService.GetBorrowRecordByIdAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        // POST: api/borrowrecords
        [HttpPost]
        public async Task<IActionResult> CreateBorrowRecord(BorrowRecord record)
        {
            var result = await _borrowService.CreateBorrowRecordAsync(record);

            if (result == "Book not found." || result == "Member not found.")
                return BadRequest(result);

            return Ok(result);
        }

        // PUT: api/borrowrecords/return/{id}
        [HttpPut("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var success = await _borrowService.ReturnBookAsync(id);
            if (!success)
                return NotFound("Borrow record not found.");

            return Ok("Book returned successfully.");
        }

        // DELETE: api/borrowrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowRecord(int id)
        {
            var deleted = await _borrowService.DeleteBorrowRecordAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
