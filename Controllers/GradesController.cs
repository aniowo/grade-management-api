using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradeManagementApi.Data;
using GradeManagementApi.Models;

namespace GradeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly GradeContext _context;

        public GradesController(GradeContext context)
        {
            _context = context;
        }

        // POST: api/Grades
        [HttpPost]
        public async Task<IActionResult> AddGrade(Grade grade)
        {
            // Check if a grade for the same StudentId and Subject already exists
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentId == grade.StudentId && g.Subject == grade.Subject);

            if (existingGrade != null)
            {
                return Conflict(new { message = "A grade for this student and subject already exists." });
            }

            // Check if the student exists
            var student = await _context.Students.FindAsync(grade.StudentId);
            if (student == null)
            {
                // Create a new student if they don't exist
                student = new Student
                {
                    StudentId = grade.StudentId,
                    StudentName = grade.Student?.StudentName,
                    StudentClass = grade.Student?.StudentClass
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                // Assign the new StudentId to the grade
                grade.StudentId = student.StudentId;
            }
            else
            {
                // Detach the existing tracked student entity
                _context.Entry(student).State = EntityState.Detached;
            }

            // Ensure the grade's Student reference is null to avoid circular reference issues
            grade.Student = null;

            // Add the grade
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            // Return a success message
            return Ok(new { message = "Grade Added Successfully" });
        }



        // GET: api/Grades/student/5
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGradesByStudentId(int studentId)
        {
            var grades = await _context.Grades.Where(g => g.StudentId == studentId).ToListAsync();

            if (grades == null || grades.Count == 0)
            {
                return NotFound();
            }

            return grades;
        }

        // PUT: api/Grades/student/5/subject/Math
        [HttpPut("student/{studentId}/subject/{subject}")]
        public async Task<IActionResult> UpdateGradeByStudentIdAndSubject(int studentId, string subject, UpdateGrade updateGrade)
        {
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.Subject == subject);

            if (existingGrade == null)
            {
                return NotFound();
            }

            existingGrade.Score = updateGrade.Score;
            existingGrade.GradeLetter = updateGrade.GradeLetter;
            existingGrade.Note = updateGrade.Note;

            _context.Entry(existingGrade).State = EntityState.Modified;

            await _context.SaveChangesAsync();
           
            return Ok(new { message = "Grade Updated Successfully" });
        }

        // DELETE: api/Grades/student/5/subject/Math
        [HttpDelete("student/{studentId}/subject/{subject}")]
        public async Task<IActionResult> DeleteGradeByStudentIdAndSubject(int studentId, string subject)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(g => g.StudentId == studentId && g.Subject == subject);

            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Grade Deleted Successfully" });
        }
    }
}