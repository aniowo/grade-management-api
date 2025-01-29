using GradeManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost]
    public async Task<IActionResult> AddGrade(Grade grade)
    {
        return await _gradeService.AddGrade(grade);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGradesByStudentId(int studentId)
    {
        var grades = await _gradeService.GetGradesByStudentId(studentId);
        if (grades == null || !grades.Any())
        {
            return NotFound();
        }
        return Ok(grades);
    }

    [HttpGet("students/{studentId}")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentById(int studentId)
    {
        var student = await _gradeService.GetStudentById(studentId);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPut("student/{studentId}/subject/{subject}")]
    public async Task<IActionResult> UpdateGradeByStudentIdAndSubject(int studentId, string subject, UpdateGrade updateGrade)
    {
        return await _gradeService.UpdateGradeByStudentIdAndSubject(studentId, subject, updateGrade);
    }

    [HttpDelete("student/{studentId}/subject/{subject}")]
    public async Task<IActionResult> DeleteGradeByStudentIdAndSubject(int studentId, string subject)
    {
        return await _gradeService.DeleteGradeByStudentIdAndSubject(studentId, subject);
    }
}
