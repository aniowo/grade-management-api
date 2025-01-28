using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeManagementApi.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string? StudentName { get; set; }

        [Required]
        public string? StudentClass { get; set; }

        // Navigation property
        public ICollection<Grade>? Grades { get; set; }
    }
}
