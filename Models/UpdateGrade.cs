namespace GradeManagementApi.Models
{
    public class UpdateGrade
    {
        public int Score { get; set; }
        public string? GradeLetter { get; set; }
        public string? Note { get; set; }
    }
}
