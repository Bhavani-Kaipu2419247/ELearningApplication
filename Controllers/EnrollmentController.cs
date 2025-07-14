using ELearningApplication.API.Models;
using ELearningApplication.API.Repositories;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[AllowAnonymous]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentController(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> EnrollStudent(Enrollment enrollment)
        {
            if (await _repository.EnrollmentExistsAsync(enrollment.StudentId, enrollment.CourseId))
                return BadRequest("Student already enrolled in this course.");

            var result = await _repository.AddEnrollmentAsync(enrollment);
            return CreatedAtAction(nameof(GetEnrollment), new { id = result.EnrollmentId }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _repository.GetEnrollmentByIdAsync(id);
            return enrollment == null ? NotFound() : Ok(enrollment);
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentsByStudent(int studentId)
        {
            var enrollments = await _repository.GetEnrollmentsByStudentAsync(studentId);
            return Ok(enrollments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgress(int id, Enrollment updatedEnrollment)
        {
            if (id != updatedEnrollment.EnrollmentId)
                return BadRequest();

            await _repository.UpdateEnrollmentAsync(updatedEnrollment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var success = await _repository.DeleteEnrollmentAsync(id);
            return success ? NoContent() : NotFound();
        }
        //private readonly IEnrollmentRepository _enrollmentRepository;
        //private static readonly ILog _logger = LogManager.GetLogger(typeof(EnrollmentController));

        //public IActionResult Enroll(int userId, int courseId)
        //{
        //    _logger.Info($"Enrollment request: userId={userId}, courseId={courseId}");

        //    try
        //    {
        //        _enrollmentRepository.EnrollUser(userId, courseId);
        //        return Ok("Enrolled successfully!");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        _logger.Warn($"Invalid input: {ex.Message}");
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        _logger.Warn($"Enrollment conflict: {ex.Message}");
        //        return Conflict(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error("Unhandled exception during enrollment", ex);
        //        throw; // Your CustomExceptionFilter will catch this
        //    }
        //}

    }
}
