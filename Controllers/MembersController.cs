using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Services;

namespace ApiProject.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MembersController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        // GET: api/members/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            var createdMember = await _memberService.CreateMemberAsync(member);
            return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);
        }

        // PUT: api/members/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member member)
        {
            if (id != member.Id)
                return BadRequest("Id mismatch.");

            var updated = await _memberService.UpdateMemberAsync(member);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/members/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var deleted = await _memberService.DeleteMemberAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
