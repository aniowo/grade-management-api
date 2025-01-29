using GradeManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

public interface IGradeService
{
    Task<IActionResult> AddGrade(Grade grade);
    Task<IEnumerable<Grade>> GetGradesByStudentId(int studentId);
    Task<Student?> GetStudentById(int studentId);
    Task<IActionResult> UpdateGradeByStudentIdAndSubject(int studentId, string subject, UpdateGrade updateGrade);
    Task<IActionResult> DeleteGradeByStudentIdAndSubject(int studentId, string subject);
}
