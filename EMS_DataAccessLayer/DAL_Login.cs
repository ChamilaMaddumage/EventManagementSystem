using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_DataAccessLayer
{
    public class DAL_Login
    {
        public bool CheckUserCredentials(string userName, string password, string userType)
        {
            try
            {
                string connetionString = DBConnection.connection();
                string getUserDetailsQuery = "EXEC [dbo].[Get_User_Details] '" + userName + "', '" + password + "', '" + userType + "'";
                using (SqlConnection conn = new SqlConnection(connetionString))
                {
                    using (SqlCommand cmd = new SqlCommand(getUserDetailsQuery, conn))
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
