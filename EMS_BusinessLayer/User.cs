using EMS_DataAccessLayer;
using EMS_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessLayer
{
    public class User
    {
        public bool UserNameExists(string userName)
        {
            DAL_User user = new DAL_User();
            try
            {
                if (user.UserNameExists(userName))
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
        public SaveResponses CreateUser(UserDetails userDetails)
        {
            DAL_User user = new DAL_User();
            SaveResponses saveResponses = new SaveResponses();
            try
            {
                if (user.CreateUser(userDetails) > 0)
                {
                    saveResponses.saveStatus = "true";
                    saveResponses.messageType = "success";
                }
                else
                {
                    saveResponses.saveStatus = "false";
                    saveResponses.messageType = "error";
                }
                return (saveResponses);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool CheckUserExist(TeamDetails teamDetails)
        {
            DAL_User user = new DAL_User();
            try
            {
                if (user.CheckUserExist(teamDetails))
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
        public bool NormalMemberExist(string userName)
        {
            DAL_User user = new DAL_User();
            try
            {
                if (user.NormalMemberExist(userName))
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
