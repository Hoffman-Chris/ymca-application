using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DataTables;

namespace ymca_application.Controllers
{
    public class ProgramParticipantController : ApiController
    {
        [System.Web.Http.Route("api/GetProgramParticipants")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetProgramParticipants(int program)
        {
            var request = HttpContext.Current.Request;

            using (var db1 = new Database("sqlserver", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var response = new Editor(db1, "ProgramParticipants", new string[] { "ProgramId", "UserId" })
                    .Field(new Field("ProgramParticipants.ProgramId")
                    )
                    .Field(new Field("ProgramParticipants.UserId")
                    )
                    .Field(new Field("AspNetUsers.FirstName")
                    )
                    .Field(new Field("AspNetUsers.LastName")
                    )
                    .Field(new Field("AspNetUsers.Email")
                    )
                    .LeftJoin("AspNetUsers", "ProgramParticipants.UserId", "=", "AspNetUsers.Id"
                    )
                    .Where("ProgramParticipants.ProgramId", program, "=")
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }

    public class DeleteProgramParticipantController : Controller
    {
        public void DeleteProgramParticipant(int program, string user)
        {
            using (SqlConnection connection = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString })
            {
                // Open connection to database.
                connection.Open();

                // Delete user from program participants table.
                using (SqlCommand command = new SqlCommand("[Delete-ProgramParticipant]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ProgramId", SqlDbType.Int).Value = program;
                    command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = user;

                    command.Prepare();
                    command.ExecuteNonQuery();
                }

                // Close connection and return success.
                connection.Close();
            }
        }
    }
}