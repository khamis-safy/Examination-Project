using Software_Engineer_Task_MVC.Models.DB_Models;

namespace Software_Engineer_Task_MVC.Models.MapperModels
{
    public class ExamQuestionsDTO
    {
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int QuestionMarks { get; set; }
        public List<QuestionChoiceDTO>? QuestionChoices { get; set; }
    }
}
