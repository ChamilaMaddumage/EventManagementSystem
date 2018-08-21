using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS_BusinessObject;

namespace EMS_DataAccessLayer
{
    public class DAL_User
    {
        public bool UserNameExists(string userName)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkUserNameAvailableQuery = "EXEC [dbo].[Check_User_Name_Available] '" + userName + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkUserNameAvailableQuery, conn))
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
        public int CreateUser(UserDetails UserDetails)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string saveUserDetails = "EXEC [dbo].[Save_User_Details] '" + UserDetails.User_Name + "','" + UserDetails.Password + "','" + UserDetails.User_Type + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(saveUserDetails, conn))
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
        public bool CheckUserExist(TeamDetails teamDetails)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkUserAvailableQuery = "EXEC [dbo].[Check_User_Available] '" + teamDetails.Team_Lead + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkUserAvailableQuery, conn))
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
        public bool NormalMemberExist(string userName)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string checkNormalUserAvailableQuery = "EXEC [dbo].[Check_Normal_User_Available] '" + userName + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(checkNormalUserAvailableQuery, conn))
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




    }
}
