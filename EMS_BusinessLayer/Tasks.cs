using EMS_BusinessObject;
using EMS_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_BusinessLayer
{
    public class Tasks
    {
        public SaveResponses CreateTask(TaskDetails taskDetails)
        {
            SaveResponses saveResponses = new SaveResponses();
            DAL_Task task = new DAL_Task();
            try
            {
                if (task.CreateTask(taskDetails) > 0)
                {
                    if (taskDetails.MemberList.Count > 0)
                    {
                        bool saveSuccess = task.SaveTaskMembers(taskDetails);
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
        public void GetTaskDetails(String userName)
        {
            DAL_Task task = new DAL_Task();
            task.GetTaskDetails(userName);
        
        }//View Team Details
        public bool TaskAvailable(string userName, string taskName)
        {
            DAL_Task task = new DAL_Task();
            try
            {
                if(task.TaskAvailable(userName, taskName))
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
        public bool EditTaskStatus(string status, string userName, string taskName)
        {
            DAL_Task task = new DAL_Task();
            try
            {
                if(task.EditTaskStatus(status, userName, taskName) > 0)
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
