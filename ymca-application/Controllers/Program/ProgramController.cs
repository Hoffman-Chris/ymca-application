using System.Configuration;
using System.Web;
using System.Web.Http;
using DataTables;

namespace ymca_application.Controllers
{
    public class ProgramController : ApiController
    {
        [Route("api/GetPrograms")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetPrograms()
        {
            var request = HttpContext.Current.Request;

            using (var db1 = new Database("sqlserver", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var response = new Editor(db1, "Program", "ProgramId")
                    .Field(new Field("Program.ProgramId")
                    )
                    .Field(new Field("Program.ProgramName")
                    )
                    .Field(new Field("Program.ProgramDescription")
                    )
                    .Field(new Field("Program.StartDate")
                        .Validator(Validation.DateFormat("M/d/yyyy"))
                        .GetFormatter(Format.DateTime("M/d/yyyy", "M/d/yyyy"))
                        .SetFormatter(Format.DateTime("M/d/yyyy", "M/d/yyyy"))
                    )
                    .Field(new Field("Program.EndDate")
                        .Validator(Validation.DateFormat("M/d/yyyy"))
                        .GetFormatter(Format.DateTime("M/d/yyyy", "M/d/yyyy"))
                        .SetFormatter(Format.DateTime("M/d/yyyy", "M/d/yyyy"))
                    )
                    .Field(new Field("Program.StartTime")
                        .Validator(Validation.DateFormat("h:mm tt"))
                        .GetFormatter(Format.DateTime("h:mm tt", "h:mm tt"))
                        .SetFormatter(Format.DateTime("h:mm tt", "h:mm tt"))
                    )
                    .Field(new Field("Program.EndTime")
                        .Validator(Validation.DateFormat("h:mm tt"))
                        .GetFormatter(Format.DateTime("h:mm tt", "h:mm tt"))
                        .SetFormatter(Format.DateTime("h:mm tt", "h:mm tt"))
                    )
                    .Field(new Field("Program.Sunday")
                    )
                    .Field(new Field("Program.Monday")
                    )
                    .Field(new Field("Program.Tuesday")
                    )
                    .Field(new Field("Program.Wednesday")
                    )
                    .Field(new Field("Program.Thursday")
                    )
                    .Field(new Field("Program.Friday")
                    )
                    .Field(new Field("Program.Saturday")
                    )
                    .Field(new Field("Program.MaxParticipants")
                    )
                    .Field(new Field("Program.CurrentParticipants")
                    )
                    .Field(new Field("Program.MemberPrice")
                    )
                    .Field(new Field("Program.NonMemberPrice")
                    )
                    .Field(new Field("Program.InstructorId")
                        .Options(new Options()
                        .Table("Instructor")
                        .Value("InstructorId")
                        .Label("InstructorFullName"))
                    )
                    .Field(new Field("Instructor.InstructorId")
                    )
                    .Field(new Field("Instructor.InstructorFirstName")
                    )
                    .Field(new Field("Instructor.InstructorMiddleName")
                    )
                    .Field(new Field("Instructor.InstructorLastName")
                    )
                    .Field(new Field("Instructor.InstructorFullName")
                    )
                    .LeftJoin("Instructor", "Program.InstructorId", "=", "Instructor.InstructorId"
                    )
                    .MJoin(new MJoin("AspNetUsers")
                        .Name("Participants")
                        .Link("AspNetUsers.Id", "ProgramParticipants.UserId")
                        .Link("ProgramParticipants.ProgramId", "Program.ProgramId")
                        .Order("AspNetUsers.FirstName")
                        .Field(new Field("AspNetUsers.FirstName"))
                        .Field(new Field("AspNetUsers.LastName"))
                    )
                    .Process(request)
                    .Data();

                return Json(response);
            }
        }
    }
}