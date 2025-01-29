using GradeManagementApi.Models;

public interface IGradeRepository
{
    Task<Grade?> GetGradeByStudentIdAndSubject(int studentId, string? subject);
    Task<IEnumerable<Grade>> GetGradesByStudentId(int studentId);
    Task AddGrade(Grade grade);
    Task AddStudent(Student student);
    Task<Student?> GetStudentById(int studentId);
    Task UpdateGrade(Grade grade);
    Task DeleteGrade(Grade grade);
    Task<bool> SaveChangesAsync();
}
