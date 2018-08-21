using EMS_BusinessObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_DataAccessLayer
{
    public class DAL_Task
    {
        public int CreateTask(TaskDetails taskDetails)
        {
            try
            {
                SaveResponses saveResponse = new SaveResponses();
                string connetionString = DBConnection.connection();
                string saveTaskDetails = "EXEC [dbo].[Save_Task_Details] '" + taskDetails.Task_Name + "','" + taskDetails.Task_Duration + "','" + taskDetails.Team_Name + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(saveTaskDetails, conn))
                    {
                        conn.Open();
                        cmd.CommandTimeout = 0;
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool SaveTaskMembers(TaskDetails taskDetails)
        {
            bool status = false;
            try
            {
                for (int i = 0; i < taskDetails.MemberList.Count; i++)
                {
                    string memberName = taskDetails.MemberList[i];
                    string connetionString = DBConnection.connection();
                    string saveTeamMembers = "EXEC [dbo].[Save_Task_Members] '" + taskDetails.Task_Name + "','" + memberName + "','going'";
                    using (SqlConnection conn = new SqlConnection(connetionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(saveTeamMembers, conn))
                        {
                            conn.Open();
                            cmd.CommandTimeout = 0;
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                status = true;
                            }
                            else
                            {
                                status = false;
                                break;
                            }
                        }
                    }


                }
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public List<TaskDetails> GetTaskDetails(String userName)
        {
            List<TaskDetails> taskDetails = new List<TaskDetails>();
            try
            {
                string connetionString = DBConnection.connection();
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    string getTaskDetailsQuery = "EXEC [dbo].[Get_Task_Details] '" + userName + "'";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getTaskDetailsQuery, conn))
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.HasRows && rdr.Read())
                        {
                            taskDetails.Add(new TaskDetails
                            {
                                Task_Name = rdr.GetString(rdr.GetOrdinal("Task_Name")),
                                Started_Date = rdr.GetDateTime(rdr.GetOrdinal("Setup_Date_Time")),
                                End_Date = rdr.GetDateTime(rdr.GetOrdinal("End_Date_Time")),
                                Status = rdr.GetString(rdr.GetOrdinal("Status")),
                            });
                        }
                        return taskDetails;
                        //Console.ForegroundColor = ConsoleColor.DarkYellow;
                        //Console.Write("Task Name   ");
                        //Console.Write("Started Date           ");
                        //Console.Write("End Date               ");
                        //Console.WriteLine("Status");
                        //Console.ResetColor();
                        //foreach (TaskDetails i in taskDetails)
                        //{
                        //    Console.Write(i.Task_Name.Trim() + "          ");
                        //    Console.Write(i.Started_Date + "   ");
                        //    Console.Write(i.End_Date + "   ");
                        //    Console.WriteLine(i.Status.Trim() + "  ");
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//View Task Details
        public bool TaskAvailable(string userName, string taskName)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkTaskAvailableQuery = "EXEC [dbo].[Check_Task_Available] '" + userName + "','" + taskName + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkTaskAvailableQuery, conn))
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            return dr.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditTaskStatus(string status, string userName, string taskName)
        {
            try
            {
                bool ss = false;
                string connetionString = DBConnection.connection();
                string editTaskStatus = "EXEC [dbo].[Edit_Task_Status] '" + status + "','" + userName + "','" + taskName + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(editTaskStatus, conn))
                    {
                        conn.Open();
                        cmd.CommandTimeout = 0;
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





    }
}
