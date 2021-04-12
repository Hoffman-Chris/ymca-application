using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using ymca_application.Models;

namespace ymca_application.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult RegisterUser(int program, string user)
        {
            using (SqlConnection connection = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString })
            {
                // Open connection to database.
                connection.Open();

                // Check if there are any free spots for the selected program.
                int freeSpots;
                using (SqlCommand command = new SqlCommand("[Get-FreeSpotsCount]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ProgramId", SqlDbType.Int).Value = program;

                    command.Prepare();
                    freeSpots = Convert.ToInt32(command.ExecuteScalar());
                }

                // If there are available spaces; else, return error.
                if (freeSpots > 0)
                {
                    // Check if the user hasn't already registered for the selected program.
                    bool eligible;
                    using (SqlCommand command = new SqlCommand("[Verify-UserEligibility]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@ProgramId", SqlDbType.Int).Value = program;
                        command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = user;

                        command.Prepare();
                        eligible = Convert.ToBoolean(command.ExecuteScalar());
                    }

                    // If not already registered for program; else, return error.
                    if (!eligible)
                    {
                        // Instantiate reader object.
                        SqlDataReader reader = null;

                        // Get the program information for the selected program.
                        ProgramModels oRegisterProgram = null;
                        using (SqlCommand command = new SqlCommand("[Get-RegisterProgramInfo]", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@ProgramId", SqlDbType.Int).Value = program;

                            command.Prepare();
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Advance to first record.
                                reader.Read();

                                // Temp variables for TryParse().
                                int tempInt;
                                DateTime tempDateTime;
                                bool tempBool;

                                // Store results in ProgramModels model.
                                oRegisterProgram = new ProgramModels();

                                // C# conditional shorthand statements — attempts to parse info gathered from SQL and fill oRegisterProgram object.
                                _ = int.TryParse(reader["ProgramId"].ToString(), out tempInt) ? oRegisterProgram.ProgramId = tempInt : oRegisterProgram.ProgramId = tempInt;
                                _ = DateTime.TryParse(reader["StartTime"].ToString(), out tempDateTime) ? oRegisterProgram.StartTime = tempDateTime : oRegisterProgram.StartTime = (DateTime?)null;
                                _ = DateTime.TryParse(reader["EndTime"].ToString(), out tempDateTime) ? oRegisterProgram.EndTime = tempDateTime : oRegisterProgram.EndTime = (DateTime?)null;
                                _ = DateTime.TryParse(reader["StartDate"].ToString(), out tempDateTime) ? oRegisterProgram.StartDate = tempDateTime : oRegisterProgram.StartDate = (DateTime?)null;
                                _ = DateTime.TryParse(reader["EndDate"].ToString(), out tempDateTime) ? oRegisterProgram.EndDate = tempDateTime : oRegisterProgram.EndDate = (DateTime?)null;
                                _ = Boolean.TryParse(reader["Monday"].ToString(), out tempBool) ? oRegisterProgram.Monday = tempBool : oRegisterProgram.Monday = (bool?)null;
                                _ = Boolean.TryParse(reader["Tuesday"].ToString(), out tempBool) ? oRegisterProgram.Tuesday = tempBool : oRegisterProgram.Tuesday = (bool?)null;
                                _ = Boolean.TryParse(reader["Wednesday"].ToString(), out tempBool) ? oRegisterProgram.Wednesday = tempBool : oRegisterProgram.Wednesday = (bool?)null;
                                _ = Boolean.TryParse(reader["Thursday"].ToString(), out tempBool) ? oRegisterProgram.Thursday = tempBool : oRegisterProgram.Thursday = (bool?)null;
                                _ = Boolean.TryParse(reader["Friday"].ToString(), out tempBool) ? oRegisterProgram.Friday = tempBool : oRegisterProgram.Friday = (bool?)null;
                                _ = Boolean.TryParse(reader["Saturday"].ToString(), out tempBool) ? oRegisterProgram.Saturday = tempBool : oRegisterProgram.Saturday = (bool?)null;
                                _ = Boolean.TryParse(reader["Sunday"].ToString(), out tempBool) ? oRegisterProgram.Sunday = tempBool : oRegisterProgram.Sunday = (bool?)null;
                            }

                            // Close reader.
                            reader.Close();
                        }

                        // Get all of the program information for each of the programs the current user is already registered for.
                        List<ProgramModels> lstUserPrograms = new List<ProgramModels>();
                        ProgramModels oTempProgram = null;
                        using (SqlCommand command = new SqlCommand("[Get-UserProgramInfo]", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = user;

                            command.Prepare();
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Loop through rows filling lstUserPrograms.
                                while (reader.Read())
                                {
                                    // Temp variables for TryParse().
                                    int tempInt;
                                    DateTime tempDateTime;
                                    bool tempBool;

                                    // Store results in ProgramModels model.
                                    oTempProgram = new ProgramModels();

                                    // C# conditional shorthand statements — attempts to parse info gathered from SQL and fill oTempProgram object.
                                    _ = int.TryParse(reader["ProgramId"].ToString(), out tempInt) ? oTempProgram.ProgramId = tempInt : oTempProgram.ProgramId = tempInt;
                                    _ = DateTime.TryParse(reader["StartTime"].ToString(), out tempDateTime) ? oTempProgram.StartTime = tempDateTime : oTempProgram.StartTime = (DateTime?)null;
                                    _ = DateTime.TryParse(reader["EndTime"].ToString(), out tempDateTime) ? oTempProgram.EndTime = tempDateTime : oTempProgram.EndTime = (DateTime?)null;
                                    _ = DateTime.TryParse(reader["StartDate"].ToString(), out tempDateTime) ? oTempProgram.StartDate = tempDateTime : oTempProgram.StartDate = (DateTime?)null;
                                    _ = DateTime.TryParse(reader["EndDate"].ToString(), out tempDateTime) ? oTempProgram.EndDate = tempDateTime : oTempProgram.EndDate = (DateTime?)null;
                                    _ = Boolean.TryParse(reader["Monday"].ToString(), out tempBool) ? oTempProgram.Monday = tempBool : oTempProgram.Monday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Tuesday"].ToString(), out tempBool) ? oTempProgram.Tuesday = tempBool : oTempProgram.Tuesday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Wednesday"].ToString(), out tempBool) ? oTempProgram.Wednesday = tempBool : oTempProgram.Wednesday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Thursday"].ToString(), out tempBool) ? oTempProgram.Thursday = tempBool : oTempProgram.Thursday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Friday"].ToString(), out tempBool) ? oTempProgram.Friday = tempBool : oTempProgram.Friday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Saturday"].ToString(), out tempBool) ? oTempProgram.Saturday = tempBool : oTempProgram.Saturday = (bool?)null;
                                    _ = Boolean.TryParse(reader["Sunday"].ToString(), out tempBool) ? oTempProgram.Sunday = tempBool : oTempProgram.Sunday = (bool?)null;

                                    // Add oTempProgram to list of user programs.
                                    lstUserPrograms.Add(oTempProgram);
                                }
                            }

                            // Close reader.
                            reader.Close();
                        }

                        // For each of the programs the current user is already registered for verify there's no overlap between selected program.
                        foreach (var oCurrentProgram in lstUserPrograms)
                        {
                            // If there's an overlap in date ranges.
                            if (oCurrentProgram.StartDate <= oRegisterProgram.EndDate && oCurrentProgram.EndDate >= oRegisterProgram.StartDate)
                            {
                                // For each day of the week.
                                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                                {
                                    // Dynamically get the value for a day of the week.
                                    string strCurrentProgramDayValue = oCurrentProgram.GetType().GetProperty(day.ToString()).GetValue(oCurrentProgram, null)?.ToString() ?? "";
                                    string strRegisterProgramDayValue = oRegisterProgram.GetType().GetProperty(day.ToString()).GetValue(oRegisterProgram, null)?.ToString() ?? "";

                                    // If there's an overlap in days of the week.
                                    if (!String.IsNullOrEmpty(strCurrentProgramDayValue) && String.Equals(strCurrentProgramDayValue, "True") &&
                                        !String.IsNullOrEmpty(strRegisterProgramDayValue) && String.Equals(strRegisterProgramDayValue, "True"))
                                    {
                                        // If there's an overlap in time.
                                        if (oCurrentProgram.StartTime < oRegisterProgram.EndTime && oCurrentProgram.EndTime > oRegisterProgram.StartTime)
                                        {
                                            // Close connection and return error.
                                            connection.Close();
                                            return Json(new { success = false, errorTitle = "Time conflict!", errorMessage = "You are already registered for a program offered at this time..." });
                                        }
                                    }
                                }
                            }
                        }

                        // Insert user into program participants table.
                        using (SqlCommand command = new SqlCommand("[Create-ProgramParticipant]", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@ProgramId", SqlDbType.Int).Value = program;
                            command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = user;

                            command.Prepare();
                            command.ExecuteNonQuery();
                        }

                        // Close connection and return success.
                        connection.Close();
                        return Json(new { success = true });
                    }
                    else
                    {
                        // Close connection and return error.
                        connection.Close();
                        return Json(new { success = false, errorTitle = "Duplicate!", errorMessage = "You are already registered for this program..." });
                    }
                }
                else
                {
                    // Close connection and return error.
                    connection.Close();
                    return Json(new { success = false, errorTitle = "Program has reached capacity!", errorMessage = "There aren't any available spaces for this program..." });
                }
            }
        }

       
        public ActionResult ViewParticipants(int program)
        {
            using (SqlConnection connection = new SqlConnection { ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString })
            {
                string queryString = "SELECT FirstName, LastName, Email FROM ProgramParticipants JOIN AspNetUsers ON ProgramParticipants.UserId = AspNetUsers.Id WHERE ProgramId = " + program.ToString();
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                List<ProgramParticipantsModel> result = new List<ProgramParticipantsModel>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var nextPerson = new ProgramParticipantsModel();
                        nextPerson.FirstName = (string)reader[0];
                        nextPerson.LastName = (string)reader[1];
                        nextPerson.Email = (string)reader[2];
                        result.Add(nextPerson);
                    }
                }
                connection.Close();
                return View("Programs", result);
            }
            }
    }
}