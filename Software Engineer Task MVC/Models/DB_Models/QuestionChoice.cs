namespace Software_Engineer_Task_MVC.Models.DB_Models
{
    public class QuestionChoice
    {
        public int ChoiceID { get; set; }
        public int QuestionID { get; set; }
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
