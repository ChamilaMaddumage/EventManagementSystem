using EMS_BusinessObject;
using EMS_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessLayer
{
    public class Team
    {
        public SaveResponses CreateTeam(TeamDetails teamDetails)
        {
            SaveResponses saveResponses = new SaveResponses();
            DAL_Team team = new DAL_Team();
            try
            {
                if (team.CreateTeam(teamDetails) > 0)
                {
                    if (teamDetails.MemberList.Count > 0)
                    {
                        bool saveSuccess = team.SaveTeamMembers(teamDetails);
                        if (saveSuccess)
                        {
                            saveResponses.saveStatus = "true";
                            saveResponses.messageType = "success";
                        }
                        else
                        {
                            saveResponses.saveStatus = "false";
                            saveResponses.messageType = "error";
                        }
                    }
                    else
                    {
                        saveResponses.saveStatus = "true";
                        saveResponses.messageType = "success";
                    }
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
        public bool CheckTeamAvailable(TaskDetails taskDetails)
        {
            DAL_Team team = new DAL_Team();
            try
            {
                if (team.CheckTeamAvailable(taskDetails))
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
        public bool CheckSameTeam(string teamName, string userName)
        {
            DAL_Team team = new DAL_Team();
            try
            {
                if (team.CheckSameTeam(teamName, userName))
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
        public List<TeamDetails> GetTeamDetails()
        {
            DAL_Team team = new DAL_Team();
            List<TeamDetails>  teamDetails = team.GetTeamDetails();
            return teamDetails;
 
        }//View Team Details
        public SaveResponses SaveTeamMembers(TeamDetails teamDetails)
        {
            SaveResponses saveResponses = new SaveResponses();
            DAL_Team team = new DAL_Team();
            try
            {
                if (teamDetails.MemberList.Count > 0)
                {
                    bool saveSuccess = team.SaveTeamMembers(teamDetails);
                    if (saveSuccess)
                    {
                        saveResponses.saveStatus = "true";
                        saveResponses.messageType = "success";
                    }
                    else
                    {
                        saveResponses.saveStatus = "false";
                        saveResponses.messageType = "error";
                    }


                }
                else
                {
                    saveResponses.saveStatus = "false";
                    saveResponses.messageType = "error";
                }
                return saveResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }




    }
}
