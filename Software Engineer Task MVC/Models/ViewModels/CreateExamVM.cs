using Software_Engineer_Task_MVC.Models.MapperModels;
using System.ComponentModel.DataAnnotations;

namespace Software_Engineer_Task_MVC.Models.ViewModels
{
    public class CreateExamVM
    {
        public string? Description { get; set; }
        [Required]
        public List<ExamQuestionsDTO> ExamQuestions { get; set; }

    }
}
