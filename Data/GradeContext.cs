using Microsoft.EntityFrameworkCore;
using GradeManagementApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GradeManagementApi.Data
{
    public class GradeContext : DbContext
    {
        public GradeContext(DbContextOptions<GradeContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId)
                .HasPrincipalKey(s => s.StudentId); // Use StudentId as the principal key

            base.OnModelCreating(modelBuilder);
        }
    }
}