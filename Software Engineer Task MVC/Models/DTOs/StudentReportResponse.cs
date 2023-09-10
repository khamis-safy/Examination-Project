namespace Software_Engineer_Task_MVC.Models.DTOs
{
    public class StudentReportResponse
    {
        public string StudentName { get; set; }
        public string ExamDescription { get; set; }
        public int ExamTotalGrade { get; set; }
        public int StudentScore { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
