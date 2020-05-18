using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalExam_AF.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace FinalExam_AF.Controllers
{
    public class ParticipantsController : ApiController
    {
        string connectStr = ConfigurationManager.ConnectionStrings["MYDB"].ConnectionString;

        //I USED THE OBJECT'S PARAMETERS SEPARATED AS INPUT ATTRIBUTES 
        //BECAUSE I WAS GETTING ERROR WITH AN OBJECT AS A INPUT PARAMETER OF THE METHOD

        [HttpPost]
        public string insertParticipant(string fn, string ln, string email, int year, int month, int day, string gender)
        {
            try
            {

                DateTime birthDate = new DateTime(year, month, day);
                string success = "Successful!";
                string sqlInsertCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Participants addingParticipants = new Participants();

                    addingParticipants.firstName = fn;
                    addingParticipants.lastName = ln;
                    addingParticipants.emailAddress = email;
                    addingParticipants.birthDate = birthDate;
                    addingParticipants.gender = gender;

                    myConn.Open();
                    sqlInsertCommand = "INSERT INTO Participants " + "(FirstName, LastName,EmailAddress, BirthDate, Gender) VALUES ('" + addingParticipants.firstName + "','" + addingParticipants.lastName + "','" + addingParticipants.emailAddress + "','" + addingParticipants.birthDate + "','" + addingParticipants.gender + "');";
                    SqlCommand myInsertCommand = new SqlCommand(sqlInsertCommand, myConn);
                    myInsertCommand.ExecuteNonQuery();
                    myConn.Close();

                }

                return success;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpPut]
        public string updateParticipant(int id, string fn, string ln, string email, int year, int month, int day, string gender)
        {
            try
            {
                DateTime birthDate = new DateTime(year, month, day);
                string success = "Successful!";
                string sqlUpdateCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Participants updateParticipants = new Participants();
                    updateParticipants.participantId = id;
                    updateParticipants.firstName = fn;
                    updateParticipants.lastName = ln;
                    updateParticipants.emailAddress = email;
                    updateParticipants.birthDate = birthDate;
                    updateParticipants.gender = gender;

                    myConn.Open();
                    sqlUpdateCommand = "UPDATE Participants " + "SET FirstName = '" + updateParticipants.firstName + "', LastName = '" + updateParticipants.lastName + "', EmailAddress = ' " + updateParticipants.emailAddress + "', BirthDate = '" + updateParticipants.birthDate + "', Gender = '" + updateParticipants.gender + "' WHERE ParticipantId =" + updateParticipants.participantId + ";";
                    SqlCommand myUpdateCommand = new SqlCommand(sqlUpdateCommand, myConn);
                    myUpdateCommand.ExecuteNonQuery();
                    myConn.Close();
                }

                return success;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpDelete]
        public string deleteParticipant(int id)
        {
            try
            {
                string success = "Successful!";

                string sqlDeleteCommand;
                using (SqlConnection myConn = new SqlConnection(connectStr))
                {
                    Participants deleteParticipants = new Participants();
                    deleteParticipants.participantId = id;


                    myConn.Open();
                    sqlDeleteCommand = "DELETE FROM Participants WHERE ParticipantId =" + deleteParticipants.participantId + ";";
                    SqlCommand myDeleteCommand = new SqlCommand(sqlDeleteCommand, myConn);
                    myDeleteCommand.ExecuteNonQuery();
                    myConn.Close();


                }
                return success;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //SEARCH FUNTIONALITY

        [HttpGet]
        public List<Participants> GetParticipantsByLastName(string lastname)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Participants> listParticipants = new List<Participants>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Participants WHERE LastName = '" + lastname + "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); ;

                    while (myReader.Read())
                    {
                        Participants tempParticipants = new Participants();
                        tempParticipants.participantId = int.Parse(myReader["ParticipantId"].ToString());
                        tempParticipants.firstName = myReader["FirstName"].ToString();
                        tempParticipants.lastName = myReader["LastName"].ToString();
                        tempParticipants.emailAddress = myReader["EmailAddress"].ToString();
                        tempParticipants.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempParticipants.gender = myReader["Gender"].ToString();

                        listParticipants.Add(tempParticipants);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listParticipants;
                }
            }
            catch (Exception ex)
            {
                List<Participants> emptyList = new List<Participants>();
                return emptyList;
            }

        }

        [HttpGet]
        public List<Participants> GetParticipantsByEmail(string email)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Participants> listParticipants = new List<Participants>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Participants WHERE EmailAddress = '" + email + "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); ;

                    while (myReader.Read())
                    {
                        Participants tempParticipants = new Participants();
                        tempParticipants.participantId = int.Parse(myReader["ParticipantId"].ToString());
                        tempParticipants.firstName = myReader["FirstName"].ToString();
                        tempParticipants.lastName = myReader["LastName"].ToString();
                        tempParticipants.emailAddress = myReader["EmailAddress"].ToString();
                        tempParticipants.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempParticipants.gender = myReader["Gender"].ToString();

                        listParticipants.Add(tempParticipants);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listParticipants;
                }
            }
            catch (Exception ex)
            {
                List<Participants> emptyList = new List<Participants>();
                return emptyList;
            }


        }


        //REPORTING

        [HttpGet]
        public List<Participants> GetParticipantsByBirthDate(int year, int month, int day)
        {
            try
            {

                DateTime date = new DateTime(year, month, day);
         



                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Participants> listParticipants = new List<Participants>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Participants WHERE BirthDate = '" + date.ToString("yyyy-MM-dd") + "';";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        Participants tempParticipants = new Participants();
                        tempParticipants.participantId = int.Parse(myReader["ParticipantId"].ToString());
                        tempParticipants.firstName = myReader["FirstName"].ToString();
                        tempParticipants.lastName = myReader["LastName"].ToString();
                        tempParticipants.emailAddress = myReader["EmailAddress"].ToString();
                        tempParticipants.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempParticipants.gender = myReader["Gender"].ToString();

                        listParticipants.Add(tempParticipants);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listParticipants;
                }
            }
            catch (Exception ex)
            {
                List<Participants> emptyList = new List<Participants>();
                return emptyList;
            }

        }





        [HttpGet]
        public List<Participants> GetParticipantsByGender(string gender)
        {
            try
            {

                using (SqlConnection myConnection = new SqlConnection(connectStr))
                {
                    List<Participants> listParticipants = new List<Participants>();
                    string sqlCommand;
                    myConnection.Open();
                    sqlCommand = "SELECT * FROM Participants WHERE Gender = '" + gender + "'";
                    SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);
                    SqlDataReader myReader = myCommand.ExecuteReader(); 

                    while (myReader.Read())
                    {
                        Participants tempParticipants = new Participants();
                        tempParticipants.participantId = int.Parse(myReader["ParticipantId"].ToString());
                        tempParticipants.firstName = myReader["FirstName"].ToString();
                        tempParticipants.lastName = myReader["LastName"].ToString();
                        tempParticipants.emailAddress = myReader["EmailAddress"].ToString();
                        tempParticipants.birthDate = Convert.ToDateTime(myReader["BirthDate"].ToString());
                        tempParticipants.gender = myReader["Gender"].ToString();

                        listParticipants.Add(tempParticipants);

                    }
                    myReader.Close();
                    myConnection.Close();

                    return listParticipants;
                }
            }
            catch (Exception ex)
            {
                List<Participants> emptyList = new List<Participants>();
                return emptyList;
            }

        }
    }
}

