using ELearningApplication.API.Models;
using ELearningApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionRepository _repository;

        public SubmissionsController(ISubmissionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Submission>>> GetSubmissions()
        {
            var results = await _repository.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Submission>> GetSubmission(int id)
        {
            var submission = await _repository.GetByIdAsync(id);
            return submission == null ? NotFound() : Ok(submission);
        }

        [HttpPost]
        public async Task<ActionResult<Submission>> SubmitAssessment(Submission submission)
        {
            var created = await _repository.CreateAsync(submission);
            return CreatedAtAction(nameof(GetSubmission), new { id = created.SubmissionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GradeSubmission(int id, Submission submission)
        {
            if (id != submission.SubmissionId)
                return BadRequest();

            await _repository.UpdateAsync(submission);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(int id)
        {
            var success = await _repository.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
