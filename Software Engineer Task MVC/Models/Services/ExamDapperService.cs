using Dapper;
using Microsoft.Data.SqlClient;
using Software_Engineer_Task_MVC.Models.DB_Models;
using Software_Engineer_Task_MVC.Models.DTOs;
using Software_Engineer_Task_MVC.Models.MapperModels;
using System.Data;

namespace Software_Engineer_Task_MVC.Models.Services
{
    public class ExamDapperService : IExamService
    {
        private readonly DapperContext db;

        public ExamDapperService(DapperContext db)
        {
            this.db = db;
        }


        public async Task<IEnumerable<Exam>> GetExams(int Take, int PageNumber)
        {
            var query = $"SELECT * FROM tblExam order by CreatedAt desc OFFSET {(PageNumber-1) * Take} ROWS FETCH NEXT {Take} ROWS ONLY";

            using (var connection = db.CreateConnection())
            {
                return await connection.QueryAsync<Exam>(query);
            }
        }

        public async Task<Exam?> CreateExam(CreateExamDTO createExam)
        {
            try
            {

                using (var connection = db.CreateConnection())
                {
                    connection.Open();
                    //begin a transaction because if anything goes wrong, all the inserted data will be reverted back
                    using (var transaction = connection.BeginTransaction())
                    {
                        //Insert the exam data first
                        var insertExamQuery = "INSERT INTO tblExam (ExamID, TotalGrads, CreatedAt, Description) VALUES" +
                            " (@ExamID, @TotalGrads, @CreatedAt, @Description)";
                        var examId = Guid.NewGuid().ToString();
                        var insertExamQueryParameters= new DynamicParameters();
                        insertExamQueryParameters.Add("ExamID", examId, DbType.String);
                        insertExamQueryParameters.Add("TotalGrads", createExam.TotalGrads,DbType.Int32);
                        insertExamQueryParameters.Add("CreatedAt", DateTime.Now, DbType.DateTime);
                        insertExamQueryParameters.Add("Description", createExam.Description, DbType.String);
                         await connection.ExecuteAsync(insertExamQuery, insertExamQueryParameters, transaction: transaction);


                        //Then Insert the exam Questions data

                        var insertQuestionQuery = "INSERT INTO tblExamQuestion ( ExamID, QuestionText, QuestionType) OUTPUT INSERTED.QuestionID VALUES" +
                                                " ( @ExamID, @QuestionText, @QuestionType)";

                        foreach (var question in createExam.ExamQuestions)
                        {
                            var insertQuestionQueryParameters = new DynamicParameters();
                            insertQuestionQueryParameters.Add("ExamID", examId, DbType.String);
                            insertQuestionQueryParameters.Add("QuestionText", question.QuestionText, DbType.String);
                            insertQuestionQueryParameters.Add("QuestionType", question.QuestionType, DbType.String);
                            var Qid=await connection.QuerySingleAsync<int>(insertQuestionQuery, insertQuestionQueryParameters, transaction: transaction);

                            //Then if there any choices Insert the question Choices data

                            if (!question.QuestionType.Equals("Essay", StringComparison.OrdinalIgnoreCase))
                            {
                                var insertQuestionChoiceQuery = "INSERT INTO tblQuestionChoice ( QuestionID, ChoiceText, IsCorrectChoice) VALUES" +
                                    " ( @QuestionID, @ChoiceText, @IsCorrectChoice)";

                                foreach (var item in question.QuestionChoices)
                                {
                                    var insertQuestionChoiceQueryParameters = new DynamicParameters();
                                    insertQuestionChoiceQueryParameters.Add("QuestionID", Qid, DbType.Int32);
                                    insertQuestionChoiceQueryParameters.Add("ChoiceText", item.ChoiceText, DbType.String);
                                    insertQuestionChoiceQueryParameters.Add("IsCorrectChoice", item.IsCorrect , DbType.Boolean);
                                    await connection.ExecuteAsync(insertQuestionChoiceQuery, insertQuestionChoiceQueryParameters, transaction: transaction);

                                }

                            }
                        }
                        transaction.Commit();
                            return new Exam
                            {
                                ExamID = examId,
                                CreatedAt = DateTime.Now,
                                Description = createExam.Description,
                                TotalGrads = createExam.TotalGrads
                            };

                    }
                }
            }
            catch (Exception)
            {
               return null;
            }

        }

        public async Task<bool> DeleteExam(string id)
        {
            var sql = $"DELETE FROM tblExam WHERE ExamID = '{id}'";

            using (var connection = db.CreateConnection())
            {
                var affectedRows =await connection.ExecuteAsync(sql);
                if (affectedRows > 0)
                    return true;
                return false;
            }


        }


        public Task StudentJoinExam(int studentID, string examID)
        {
            throw new NotImplementedException();
        }

        public Task StudentFinishExam(int studentID, string examID, List<StudentsAnswer> answers)
        {
            throw new NotImplementedException();
        }

        public async Task<List< StudentReportResponse>> GetStudentReport(int studentId)
        {
            var query = $"SELECT Name,Description,TotalGrade,TotalGrads,JoinedAt,FinishedAt " +
                $"FROM tblStudentsInExam I INNER JOIN tblExam E ON I.ExamID = E.ExamID INNER JOIN tblStudent S ON S.StudentID=I.StudentID" +
                $" WHERE I.StudentID={studentId} order by I.JoinedAt desc";

            using (var connection = db.CreateConnection())
            {
                var res= await connection.QueryAsync(query);
                List<StudentReportResponse> studentReportResponses= new List<StudentReportResponse>();
                foreach (var item in res)
                {
                    studentReportResponses.Add(new StudentReportResponse
                    {
                        ExamDescription = item.Description,
                        ExamTotalGrade = item.TotalGrads,
                        FinishedAt = item.FinishedAt,
                        JoinedAt = item.JoinedAt,
                        StudentName = item.Name,
                        StudentScore = item.TotalGrade,
                    });
                }
                return studentReportResponses;
            }
        }
    }
}
