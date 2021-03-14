using System.Configuration;
using System.Web;
using System.Web.Http;
using DataTables;

namespace ymca_application.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/GetUsers")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetUsers()
        {
            var request = HttpContext.Current.Request;

            using (var db1 = new Database("sqlserver", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var response = new Editor(db1, "AspNetUsers", "Id")
                    .Field(new Field("AspNetUsers.FirstName")
                    )
                    .Field(new Field("AspNetUsers.LastName")
                    )
                    .Field(new Field("AspNetUsers.Email")
                    )
                    .Field(new Field("AspNetUsers.PhoneNumber")
                    )
                    .Field(new Field("AspNetUsers.Address1")
                    )
                    .Field(new Field("AspNetUsers.Address2")
                    )
                    .Field(new Field("AspNetUsers.City")
                    )
                    .Field(new Field("AspNetUsers.State")
                    )
                    .Field(new Field("AspNetUsers.Zip")
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
}