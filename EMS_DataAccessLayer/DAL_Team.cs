using EMS_BusinessObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_DataAccessLayer
{
    public class DAL_Team
    {
        public int CreateTeam(TeamDetails teamDetails)
        {
            try
            {
                SaveResponses saveResponse = new SaveResponses();
                string connetionString = DBConnection.connection();
                string saveTeamDetails = "EXEC [dbo].[Save_Team_Details] '" + teamDetails.Team_Name + "','" + teamDetails.Team_Lead + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(saveTeamDetails, conn))
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
        public bool SaveTeamMembers(TeamDetails teamDetails)
        {
            bool status = false;
            try
            {
                for (int i = 0; i < teamDetails.MemberList.Count; i++)
                {
                    string memberName = teamDetails.MemberList[i];
                    string connetionString = DBConnection.connection();
                    string saveTeamMembers = "EXEC [dbo].[Save_Team_Members] '" + teamDetails.Team_Name + "','" + memberName + "'";
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
        public bool CheckTeamAvailable(TaskDetails taskDetails)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkTeamAvailableQuery = "EXEC [dbo].[Check_Team_Available] '" + taskDetails.Team_Name + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkTeamAvailableQuery, conn))
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
        public bool CheckSameTeam(string teamName, string userName)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkSameTeamQuery = "EXEC [dbo].[Check_Same_Team] '" + teamName + "','" + userName + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkSameTeamQuery, conn))
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
        public List<TeamDetails> GetTeamDetails()
        {
            List<TeamDetails> teamDetails = new List<TeamDetails>();
            try
            {
                string connetionString = DBConnection.connection();
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    string getTeamDetailsQuery = "select * from Team";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(getTeamDetailsQuery, conn))
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.HasRows && rdr.Read())
                        {
                            teamDetails.Add(new TeamDetails
                            {
                                Team_Name = rdr.GetString(rdr.GetOrdinal("Team_Name")),
                                Team_Lead = rdr.GetString(rdr.GetOrdinal("Team_Lead")),
                            });
                        }
                        return teamDetails;
                        //Console.ForegroundColor = ConsoleColor.DarkYellow;
                        //Console.Write("Team Name   ");
                        //Console.WriteLine("Team Lead");
                        //Console.ResetColor();
                        //foreach (TeamDetails i in teamDetails)
                        //{
                        //    Console.Write(i.Team_Name.Trim() +"       ");
                        //    Console.WriteLine(i.Team_Lead);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//View Team Details





    }
}
