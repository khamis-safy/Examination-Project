using Software_Engineer_Task_MVC.Models.DB_Models;
using Software_Engineer_Task_MVC.Models.DTOs;
using Software_Engineer_Task_MVC.Models.MapperModels;

namespace Software_Engineer_Task_MVC.Models.Services
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetExams(int Take, int PageNumber);

        Task<Exam?> CreateExam(CreateExamDTO createExam);
        Task<bool> DeleteExam(string id);
        Task StudentJoinExam(int studentID, string examID);
        Task StudentFinishExam(int studentID, string examID, List<StudentsAnswer> answers);
        Task<List<StudentReportResponse>> GetStudentReport(int studentId);
    }
}
