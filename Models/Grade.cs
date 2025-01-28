using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeManagementApi.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Subject { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public string? GradeLetter { get; set; }

        public string? Note { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]

        public Student? Student { get; set; }
    }
}