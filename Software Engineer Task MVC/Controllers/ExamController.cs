using Microsoft.AspNetCore.Mvc;
using Software_Engineer_Task_MVC.Models;
using Software_Engineer_Task_MVC.Models.DB_Models;
using Software_Engineer_Task_MVC.Models.MapperModels;
using Software_Engineer_Task_MVC.Models.Services;
using Software_Engineer_Task_MVC.Models.ViewModels;
using System.Diagnostics;
using System.Reflection;

namespace Software_Engineer_Task_MVC.Controllers
{
    public class ExamController : Controller
    {
        private readonly ILogger<ExamController> _logger;
        private readonly IExamService examService;

        public ExamController(ILogger<ExamController> logger, IExamService examService)
        {
            _logger = logger;
            this.examService = examService;
        }

        public async Task<IActionResult> Index()
        {
            var exams = await examService.GetExams(100, 1);

            return View(exams);
        }

        public IActionResult CreateExam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamVM createExam)
        {
            var examdto = new CreateExamDTO
            {
                Description = createExam.Description,
                ExamQuestions = createExam.ExamQuestions,
                TotalGrads = 0
            };
            foreach (var item in createExam.ExamQuestions)
            {
                examdto.TotalGrads += item.QuestionMarks;
            }
            await examService.CreateExam(examdto);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var res = await examService.DeleteExam(id);
            if (res)
                return RedirectToAction("Index");
            return BadRequest();
        }


        //Pls check the comments inside the following 3 Endpoints

        [HttpPost]
        public async Task<IActionResult> StudentJoinExam(int studentID, string examID)
        {
            //the following function responsible for creating new row in tblStudentsInExam,
            //and put the datetime of joining the exam
            //The function is not implemented, i just showing you the algorithm
            await examService.StudentJoinExam(studentID, examID);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> StudentFinishExam(int studentID, string examID, List<StudentsAnswer> answers)
        {
            //the following function responsible for:
            //1- calculate the total grades by indicate if the answer is correct or not by compare his answer with the correct answer that the admin created already
            //2- modify the Finish datetime in the in tblStudentsInExam row that already created,
            //The function is not implemented, i just showing you the algorithm
            await examService.StudentFinishExam(studentID, examID, answers);

            return Ok();
        }




        [HttpGet("{studentID}")]
        public async Task<IActionResult> GetStudentReport(int studentID)
        {
            //this function responsible for getting the students records from the tblStudentsInExam and tblExam and tblStudent
            //
            //
            //this function is implemented
            //
            //
            var res= await examService.GetStudentReport(studentID);

            return View(res);

        }



    }
}