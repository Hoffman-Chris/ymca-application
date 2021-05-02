using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DataTables;

namespace ymca_application.Controllers
{
    public class APIUserController : ApiController
    {
        [System.Web.Http.Route("api/GetUsers")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetUsers()
        {
            var request = HttpContext.Current.Request;

            using (var db1 = new Database("sqlserver", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var response = new Editor(db1, "AspNetUsers", "Id")
                    .Field(new Field("AspNetUsers.Id")
                    )
                    .Field(new Field("AspNetUsers.FirstName")
                    )
                    .Field(new Field("AspNetUsers.LastName")
                    )
                    .Field(new Field("AspNetUsers.Email")
                    )
                    .Field(new Field("AspNetUsers.PhoneNumber")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.Address1")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.Address2")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.City")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.State")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.Zip")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.Active")
                    )
                    .Field(new Field("AspNetUsers.LockoutEndDateUtc")
                    .SetFormatter(Format.IfEmpty(null))
                    .GetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("AspNetUsers.JoinDate")
                    )
                    .Field(new Field("AspNetUsers.Role")
                    .Options(new Options()
                    .Table("AspNetRoles")
                    .Value("Id")
                    .Label("Name"))
                    )
                    .Field(new Field("AspNetRoles.Id")
                    .Validator(Validation.NotEmpty())
                    )
                    .Field(new Field("AspNetRoles.Name")
                    .Validator(Validation.NotEmpty())
                    )
                    .LeftJoin("AspNetRoles", "AspNetUsers.Role", "=", "AspNetRoles.Id"
                    )
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }

    public class MVCUserController : Controller
    {
        public void DisableUser(string UserId)
        {
            using (SqlConnection connection = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString })
            {
                // Open connection to database.
                connection.Open();

                // Execute stored procedure to disable a user.
                using (SqlCommand command = new SqlCommand("[Disable-User]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = UserId;

                    command.Prepare();
                    command.ExecuteNonQuery();
                }

                // Close connection.
                connection.Close();
            }
        }
    }
}