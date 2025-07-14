using ELearningApplication.API.Models;
using ELearningApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentsController(IAssessmentRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments()
        {
            //var token=Request.Headers.Authorization[0];
            var items = await _repository.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id)
        {
            var assessment = await _repository.GetByIdAsync(id);
            return assessment == null ? NotFound() : Ok(assessment);
        }
        [HttpPost]
        public async Task<ActionResult<Assessment>> CreateAssessment(Assessment assessment)
        {
            var created = await _repository.CreateAsync(assessment);
            return CreatedAtAction(nameof(GetAssessment), new { id = created.AssessmentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssessment(int id, Assessment assessment)
        {
            if (id != assessment.AssessmentId)
                return BadRequest();

            await _repository.UpdateAsync(assessment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
