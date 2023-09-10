using Microsoft.Data.SqlClient;
using System.Data;

namespace Software_Engineer_Task_MVC.Models
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionString"];
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }

}
