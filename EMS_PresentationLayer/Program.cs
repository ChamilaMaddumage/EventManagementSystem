using EMS_BusinessLayer;
using EMS_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS_PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            login:
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("---------- Login ----------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[[Admin (admin), Lead (lead), Normal (normal)]]");
            Console.ResetColor();
            Console.Write("User Type : ");
            string userType = Console.ReadLine();
            if (!(String.Equals(userType, "admin", StringComparison.OrdinalIgnoreCase) || String.Equals(userType, "lead", StringComparison.OrdinalIgnoreCase) || String.Equals(userType, "normal", StringComparison.OrdinalIgnoreCase)))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Select the correct type");
                Console.ResetColor();
                goto login;
            }
            Console.Write("User Name : ");
            string userName = Console.ReadLine();
            Console.Write("Password : ");
            string password = Console.ReadLine();
            Login login = new Login();
            bool loginResponse = login.CheckUserCredentials(userName, password, userType);
            if (loginResponse)
            {
                startMainMenu:
                if (String.Equals(userType, "admin", StringComparison.OrdinalIgnoreCase))//Start admin user functions
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[[Create new users (cnu), Create a team (cat), add team members (atm), Create a task (cata), View team details(vtd), Logout(lo)]]");
                    Console.ResetColor();
                    string functionType = Console.ReadLine();
                    UserDetails userDetails = new UserDetails();
                    User user = new User();
                    if (String.Equals(functionType, "cnu", StringComparison.OrdinalIgnoreCase))//start create new user function
                    {
                        Console.WriteLine("------- Create Users -------");
                        enterUserName:
                        Console.Write("User Name : ");
                        userDetails.User_Name = Console.ReadLine();
                        if (user.UserNameExists(userDetails.User_Name))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("User already exists. Try again");
                            Console.ResetColor();
                            goto enterUserName;
                        }
                        Console.Write("Password : ");
                        userDetails.Password = Console.ReadLine();
                        selectUserType1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[[Admin (admin), Lead (lead), Normal (normal)]]");
                        Console.ResetColor();
                        Console.Write("User Type : ");
                        userDetails.User_Type = Console.ReadLine();
                        if (!(String.Equals(userDetails.User_Type, "admin", StringComparison.OrdinalIgnoreCase) || String.Equals(userDetails.User_Type, "lead", StringComparison.OrdinalIgnoreCase) || String.Equals(userDetails.User_Type, "normal", StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid command. Try again");
                            Console.ResetColor();
                            goto selectUserType1;
                        }

                        SaveResponses saveResponses = user.CreateUser(userDetails);
                        if (saveResponses.saveStatus == "true")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("User Creation Success");
                            Console.ResetColor(); goto startMainMenu;
                        }
                        else if (saveResponses.saveStatus == "false")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("User creation error. Try again");
                            Console.ResetColor();
                            goto startMainMenu;

                        }
                    }
                    else if (String.Equals(functionType, "cat", StringComparison.OrdinalIgnoreCase))//start create a team function
                    {
                        startCreateTeam:
                        TeamDetails teamDetails = new TeamDetails();
                        Console.WriteLine("------- Create a Team-------");
                        Console.WriteLine("Team Name : ");
                        teamDetails.Team_Name = Console.ReadLine();
                        enterTeamLead:
                        Console.WriteLine("Team Lead : ");
                        teamDetails.Team_Lead = Console.ReadLine();
                        bool userExist = user.CheckUserExist(teamDetails);
                        if (!userExist)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Team Lead does not exists.Please enter registered user name");
                            Console.ResetColor();
                            goto enterTeamLead;
                        }
                        List<String> memberList = new List<string>();
                        saveoradd:
                        Console.WriteLine("Add team members or save details");
                        saveorAdd:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("[[Save (save), Add (add)]]");
                        Console.ResetColor();
                        string commandType = Console.ReadLine();
                        if (String.Equals(commandType, "add", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Name : ");
                            string normalm = Console.ReadLine();
                            if (user.NormalMemberExist(normalm))
                            {
                                memberList.Add(normalm);
                                goto saveoradd;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Member does not exist");
                                Console.ResetColor();
                                goto saveoradd;
                            }

                        }
                        else if (String.Equals(commandType, "save", StringComparison.OrdinalIgnoreCase))
                        {
                            teamDetails.MemberList = memberList;
                            Team team = new Team();
                            SaveResponses saveResponses = team.CreateTeam(teamDetails);
                            if (saveResponses.saveStatus == "true")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Team Creation Success");
                                Console.ResetColor();
                                goto startMainMenu;

                            }
                            else if (saveResponses.saveStatus == "false")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("Team Creation Error. Try again");
                                Console.ResetColor();
                                goto startCreateTeam;


                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid command. Try again");
                            Console.ResetColor();
                            goto saveorAdd;
                        }
                    }
                    else if (String.Equals(functionType, "cata", StringComparison.OrdinalIgnoreCase))//start create a task function
                    {
                        startCreateTask:
                        TaskDetails taskDetails = new TaskDetails();
                        Console.WriteLine("------- Create a Task -------");
                        Console.Write("Task Name : ");
                        taskDetails.Task_Name = Console.ReadLine();
                        enterEndDate:
                        Console.Write("End Date : ");
                        taskDetails.Task_Duration = Console.ReadLine();
                        DateTime userDateTime;
                        if (!(DateTime.TryParse(taskDetails.Task_Duration, out userDateTime)))
                        {
                            Console.WriteLine("You have entered an incorrect value. Try again");
                            goto enterEndDate;
                            // Console.WriteLine("The day of the week is: " + userDateTime.DayOfWeek);

                        }
                        //Console.WriteLine("The day of the week is: " + userDateTime.DayOfWeek);///////////////////////////////////////
                        addTeamName:
                        Console.Write("Team Name : ");
                        taskDetails.Team_Name = Console.ReadLine();
                        Team team = new Team();
                        bool teamAvailable = team.CheckTeamAvailable(taskDetails);
                        if (teamAvailable)
                        {
                            List<String> taskMemberList = new List<string>();
                            taskSaveoradd:
                            Console.WriteLine("Add team members or save details");
                            saveorAddTask:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[[Save (save), Add (add)]]");
                            Console.ResetColor();
                            string commandType = Console.ReadLine();
                            if (String.Equals(commandType, "add", StringComparison.OrdinalIgnoreCase))//////////////////
                            {
                                Console.Write("Name : ");
                                string name = Console.ReadLine();
                                if (user.UserNameExists(name) && team.CheckSameTeam(taskDetails.Team_Name, name))
                                {
                                    taskMemberList.Add(name);
                                    goto taskSaveoradd;

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("User does not exists. Try again");
                                    Console.ResetColor();
                                    goto taskSaveoradd;
                                    //////////////////////////////
                                }

                                //name should be validated
                            }
                            else if (String.Equals(commandType, "save", StringComparison.OrdinalIgnoreCase))
                            {
                                taskDetails.MemberList = taskMemberList;
                                Tasks task = new Tasks();
                                SaveResponses saveResponses = task.CreateTask(taskDetails);
                                if (saveResponses.saveStatus == "true")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Task Creation Success");
                                    Console.ResetColor();
                                    goto startMainMenu;

                                }
                                else if (saveResponses.saveStatus == "false")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write("Task Creation Error. Try again");
                                    Console.ResetColor();
                                    goto startCreateTask;


                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid command. Try again");
                                Console.ResetColor();
                                goto saveorAddTask;
                            }

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Team Name not available. Try again");
                            Console.ResetColor();
                            goto addTeamName;
                        }
                    }
                    else if (String.Equals(functionType, "vtd", StringComparison.OrdinalIgnoreCase))
                    {
                        Team team = new Team();
                        team.GetTeamDetails();
                        goto startMainMenu;

                    }
                    else if (String.Equals(functionType, "atm", StringComparison.OrdinalIgnoreCase))
                    {
                        addTeamMembers:
                        TeamDetails teamDetails = new TeamDetails();
                        TaskDetails taskDetails = new TaskDetails();
                        Team team = new Team();
                        addTeamNameLead:
                        Console.Write("Team Name : ");
                        taskDetails.Team_Name = Console.ReadLine();
                        teamDetails.Team_Name = taskDetails.Team_Name;
                        if (team.CheckTeamAvailable(taskDetails))
                        {
                            List<String> memberList = new List<string>();
                            saveoradd:
                            Console.WriteLine("Add team members or save details");
                            saveorAdd:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[[Save (save), Add (add)]]");
                            Console.ResetColor();
                            string commandType = Console.ReadLine();
                            if (String.Equals(commandType, "add", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Write("Name : ");
                                string normalm = Console.ReadLine();
                                if (user.NormalMemberExist(normalm))
                                {
                                    memberList.Add(normalm);
                                    goto saveoradd;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Member does not exist");
                                    Console.ResetColor();
                                    goto saveoradd;
                                }

                            }
                            else if (String.Equals(commandType, "save", StringComparison.OrdinalIgnoreCase))
                            {
                                teamDetails.MemberList = memberList;
                                SaveResponses saveResponses = team.SaveTeamMembers(teamDetails);
                                if (saveResponses.saveStatus == "true")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Member allocation Success");
                                    Console.ResetColor();
                                    goto startMainMenu;

                                }
                                else if (saveResponses.saveStatus == "false")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Member allocation Error. Try again");
                                    Console.ResetColor();
                                    goto addTeamMembers;


                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid command. Try again");
                                Console.ResetColor();
                                goto saveorAdd;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Team Name not available. Try again");
                            Console.ResetColor();
                            goto addTeamNameLead;
                        }

                    }
                    else if (String.Equals(functionType, "lo", StringComparison.OrdinalIgnoreCase))
                    {
                        goto login;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid command. Try again");
                        Console.ResetColor();
                        goto startMainMenu;
                    }
                }//usertype admin end
                else if (String.Equals(userType, "lead", StringComparison.OrdinalIgnoreCase))
                {
                    startMainMenuLead:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[[Create a task (cata), View team details(vtd), Logout(lo)]]");
                    Console.ResetColor();
                    string functionTypeLead = Console.ReadLine();
                    if (String.Equals(functionTypeLead, "cata", StringComparison.OrdinalIgnoreCase))
                    {
                        startCreateTaskLead:
                        TaskDetails taskDetails = new TaskDetails();
                        Console.WriteLine("------- Create a Task -------");
                        Console.Write("Task Name : ");
                        taskDetails.Task_Name = Console.ReadLine();
                        enterEndDate:
                        Console.Write("End Date : ");
                        taskDetails.Task_Duration = Console.ReadLine();
                        DateTime userDateTime;
                        if (!(DateTime.TryParse(taskDetails.Task_Duration, out userDateTime)))
                        {
                            Console.WriteLine("You have entered an incorrect value. Try again");
                            goto enterEndDate;
                            // Console.WriteLine("The day of the week is: " + userDateTime.DayOfWeek);
                        }
                        //Console.WriteLine("The day of the week is: " + userDateTime.DayOfWeek);///////////////////////////////////////
                        addTeamNameLead:
                        Console.Write("Team Name : ");
                        taskDetails.Team_Name = Console.ReadLine();
                        Team team = new Team();
                        bool teamAvailable = team.CheckTeamAvailable(taskDetails);
                        if (teamAvailable)
                        {
                            List<String> taskMemberList = new List<string>();
                            taskSaveoradLead:
                            Console.WriteLine("Add team members or save details");
                            saveorAddTask:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[[Save (save), Add (add)]]");
                            Console.ResetColor();
                            string commandType = Console.ReadLine();
                            if (String.Equals(commandType, "add", StringComparison.OrdinalIgnoreCase))////////////////////////////////////////////
                            {
                                Console.Write("Name : ");
                                string name = Console.ReadLine();
                                User user = new User();
                                if (user.UserNameExists(name) && team.CheckSameTeam(taskDetails.Team_Name, name))
                                {
                                    taskMemberList.Add(name);
                                    goto taskSaveoradLead;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("User does not exists. Try again");
                                    Console.ResetColor();
                                    goto taskSaveoradLead;
                                }
                            }
                            else if (String.Equals(commandType, "save", StringComparison.OrdinalIgnoreCase))
                            {
                                taskDetails.MemberList = taskMemberList;
                                Tasks task = new Tasks();
                                SaveResponses saveResponses = task.CreateTask(taskDetails);
                                if (saveResponses.saveStatus == "true")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Task Creation Success");
                                    Console.ResetColor();
                                    goto startMainMenuLead;
                                }
                                else if (saveResponses.saveStatus == "false")
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.Write("Task Creation Error. Try again");
                                    Console.ResetColor();
                                    goto startCreateTaskLead;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Invalid command. Try again");
                                Console.ResetColor();
                                goto saveorAddTask;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Team Name not available. Try again");
                            Console.ResetColor();
                            goto addTeamNameLead;
                        }
                    }
                    else if (String.Equals(functionTypeLead, "vtd", StringComparison.OrdinalIgnoreCase))
                    {
                        Team team = new Team();
                        team.GetTeamDetails();
                        goto startMainMenuLead;
                    }
                    else if (String.Equals(functionTypeLead, "lo", StringComparison.OrdinalIgnoreCase))
                    {
                        goto login;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid command. Try again");
                        Console.ResetColor();
                        goto startMainMenuLead;
                    }
                }//usertype lead end
                else if (String.Equals(userType, "normal", StringComparison.OrdinalIgnoreCase))//start normal user functions
                {
                    normalMenuStart:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("[[View tasks (vt), Edit task status (ets), Logout(lo)]]");
                    Console.ResetColor();
                    string functionTypeNormal = Console.ReadLine();
                    if (String.Equals(functionTypeNormal, "vt", StringComparison.OrdinalIgnoreCase))
                    {
                        Tasks task = new Tasks();
                        task.GetTaskDetails(userName);
                        Console.WriteLine();
                        goto normalMenuStart;
                    }
                    else if (String.Equals(functionTypeNormal, "ets", StringComparison.OrdinalIgnoreCase))//Edit task status function start
                    {
                        startEditTask:
                        Tasks task = new Tasks();
                        Console.WriteLine("Task Name : ");
                        string taskName = Console.ReadLine();
                        if (task.TaskAvailable(userName, taskName))
                        {
                            addStatus:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[[going (going), completed (completed)]]");
                            Console.ResetColor();
                            Console.WriteLine("Status : ");
                            string status = Console.ReadLine();
                            if (!(String.Equals(status, "going", StringComparison.OrdinalIgnoreCase) || String.Equals(status, "completed", StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Error. Try again");
                                Console.ResetColor();
                                goto addStatus;
                            }
                            if (task.EditTaskStatus(status, userName, taskName))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Update Success");
                                Console.ResetColor();
                                goto normalMenuStart;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Error");
                                Console.ResetColor();
                                goto startEditTask;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Task not available");
                            Console.ResetColor();
                            goto startEditTask;
                        }
                    }
                    else if (String.Equals(functionTypeNormal, "lo", StringComparison.OrdinalIgnoreCase))
                    {
                        goto login;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid command. Try again");
                        Console.ResetColor();
                        goto normalMenuStart;
                    }
                }//usertype normal end
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid Credentials. Try again");
                Console.ResetColor();
                goto login;
            }
        }
    }
}
