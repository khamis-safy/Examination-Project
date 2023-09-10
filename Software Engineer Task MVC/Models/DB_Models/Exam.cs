namespace Software_Engineer_Task_MVC.Models.DB_Models
{
    public class Exam
    {
        public string ExamID { get; set; }
        public int TotalGrads { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
    }
}
