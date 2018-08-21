using EMS_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessLayer
{
    public class Login
    {
        public bool CheckUserCredentials(string userName, string password, string userType)
        {
            DAL_Login login = new DAL_Login();
            try
            {
                bool LoginResponse = login.CheckUserCredentials(userName, password, userType);
                if (LoginResponse)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
