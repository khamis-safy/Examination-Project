using Software_Engineer_Task_MVC.Models.DB_Models;

namespace Software_Engineer_Task_MVC.Models.MapperModels
{
    public class CreateExamDTO
    {
        public string? Description { get; set; }
        public int TotalGrads { get; set; }
        public List<ExamQuestionsDTO> ExamQuestions { get; set; }
    }
}
