using GradeManagementApi.Data;
using GradeManagementApi.Models;
using Microsoft.EntityFrameworkCore;

public class GradeRepository : IGradeRepository
{
    private readonly GradeContext _context;

    public GradeRepository(GradeContext context)
    {
        _context = context;
    }

    public async Task<Grade?> GetGradeByStudentIdAndSubject(int studentId, string? subject)
    {
        return await _context.Grades.FirstOrDefaultAsync(g => g.StudentId == studentId && g.Subject == subject);
    }

    public async Task<IEnumerable<Grade>> GetGradesByStudentId(int studentId)
    {
        return await _context.Grades.Where(g => g.StudentId == studentId).AsNoTracking().ToListAsync();
    }

    public async Task AddGrade(Grade grade)
    {
        await _context.Grades.AddAsync(grade);
    }
    public async Task AddStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGrade(Grade grade)
    {
        _context.Entry(grade).State = EntityState.Modified;
    }

    public async Task DeleteGrade(Grade grade)
    {
         _context.Grades.Remove(grade);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Student?> GetStudentById(int studentId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
    }
}
