using GradeManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public async Task<IActionResult> AddGrade(Grade grade)
    {
        var existingGrade = await _gradeRepository.GetGradeByStudentIdAndSubject(grade.StudentId, grade.Subject);
        if (existingGrade != null)
        {
            return new ConflictObjectResult(new { message = "A grade for this student and subject already exists." });
        }

        var student = await _gradeRepository.GetStudentById(grade.StudentId);
        if (student == null)
        {
            student = new Student
            {
                StudentId = grade.StudentId,
                StudentName = grade.Student?.StudentName,
                StudentClass = grade.Student?.StudentClass
            };
            await _gradeRepository.AddStudent(student);
        }

        grade.Student = null;

        await _gradeRepository.AddGrade(grade);
        await _gradeRepository.SaveChangesAsync();

        return new OkObjectResult(new { message = "Grade Added Successfully" });
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentId(int studentId)
    {
        return await _gradeRepository.GetGradesByStudentId(studentId);
    }

    public async Task<Student?> GetStudentById(int studentId)
    {
        return await _gradeRepository.GetStudentById(studentId);
    }

    public async Task<IActionResult> UpdateGradeByStudentIdAndSubject(int studentId, string subject, UpdateGrade updateGrade)
    {
        var existingGrade = await _gradeRepository.GetGradeByStudentIdAndSubject(studentId, subject);
        if (existingGrade == null)
        {
            return new NotFoundResult();
        }

        existingGrade.Score = updateGrade.Score;
        existingGrade.GradeLetter = updateGrade.GradeLetter;
        existingGrade.Note = updateGrade.Note;

        await _gradeRepository.UpdateGrade(existingGrade);
        await _gradeRepository.SaveChangesAsync();

        return new OkObjectResult(new { message = "Grade Updated Successfully" });
    }

    public async Task<IActionResult> DeleteGradeByStudentIdAndSubject(int studentId, string subject)
    {
        var grade = await _gradeRepository.GetGradeByStudentIdAndSubject(studentId, subject);
        if (grade == null)
        {
            return new NotFoundResult();
        }

        await _gradeRepository.DeleteGrade(grade);
        await _gradeRepository.SaveChangesAsync();

        return new OkObjectResult(new { message = "Grade Deleted Successfully" });
    }
}
