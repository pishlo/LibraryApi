using Microsoft.AspNetCore.Mvc;
using ApiProject.Dtos;
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        // GET: api/members/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMember(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto dto)
        {
            var createdMember = await _memberService.CreateMemberAsync(dto);
            return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);
        }

        // PUT: api/members/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberDto dto)
        {
            var updated = await _memberService.UpdateMemberAsync(id, dto);
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