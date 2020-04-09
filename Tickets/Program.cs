using System;
using System.IO;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Tickets
{
    class MainClass
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");

            string bFile = "../../../Tickets.csv";
            string eFile = "../../../Enhancements.csv";
            string tFile = "../../../Task.csv";

            BugFile bugFile = new BugFile(bFile);
            EnhancementFile enhancementFile = new EnhancementFile(eFile);
            TaskFile taskFile = new TaskFile(tFile);

            int answer;
            do
            {
                Console.WriteLine("1. Add Bug/Defect\n2. Display Bugs/Defects\n3. Add Enhancement\n" +
                    "4. Display Enhancements\n5. Add Task\n6. Display Tasks\nPress Enter to go to Search Application.");
                int.TryParse(Console.ReadLine(), out answer);
                switch (answer)
                {
                    case 1:
                        {
                            string resp = "Y";
                            do
                            {
                                if (resp == "Y")
                                {
                                    Bug bug = new Bug();
                                    Console.WriteLine("Enter Summary.");
                                    bug.summary = Console.ReadLine();
                                    Console.WriteLine("Enter Status.");
                                    bug.status = Console.ReadLine();
                                    Console.WriteLine("Enter Priority.");
                                    bug.priority = Console.ReadLine();
                                    Console.WriteLine("Enter Submitter.");
                                    bug.submitter = Console.ReadLine();
                                    Console.WriteLine("Enter Assigned.");
                                    bug.assigned = Console.ReadLine();
                                    Console.WriteLine("How many people are watching?");
                                    Int32.TryParse(Console.ReadLine(), out int watching);
                                    for (int i = 0; i < watching; i++)
                                    {
                                        Console.WriteLine($"Enter watcher {i + 1}");
                                        bug.watching.Add(Console.ReadLine());
                                    }
                                    Console.WriteLine("Enter Severity.");
                                    bug.severity = Console.ReadLine();
                                    bugFile.AddBug(bug);
                                }
                                Console.WriteLine("Enter another bug/defect (Y/N)?");
                                resp = Console.ReadLine().ToUpper();
                            } while (resp == "Y");
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine(bugFile.header);
                            foreach (Bug b in bugFile.Bugs)
                            {
                                Console.WriteLine(b.Display());
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        {
                            string resp = "Y";
                            do
                            {
                                if (resp == "Y")
                                {
                                    Enhancement enhancement = new Enhancement();
                                    Console.WriteLine("Enter Summary.");
                                    enhancement.summary = Console.ReadLine();
                                    Console.WriteLine("Enter Status.");
                                    enhancement.status = Console.ReadLine();
                                    Console.WriteLine("Enter Priority.");
                                    enhancement.priority = Console.ReadLine();
                                    Console.WriteLine("Enter Submitter.");
                                    enhancement.submitter = Console.ReadLine();
                                    Console.WriteLine("Enter Assigned.");
                                    enhancement.assigned = Console.ReadLine();
                                    Console.WriteLine("How many people are watching?");
                                    Int32.TryParse(Console.ReadLine(), out int watching);
                                    for (int i = 0; i < watching; i++)
                                    {
                                        Console.WriteLine($"Enter watcher {i + 1}");
                                        enhancement.watching.Add(Console.ReadLine());
                                    }
                                    Console.WriteLine("Enter Software.");
                                    enhancement.software = Console.ReadLine();
                                    Console.Write("Enter Cost.\n$");
                                    double cost;
                                    double.TryParse(Console.ReadLine(), out cost);
                                    enhancement.cost = cost;
                                    Console.WriteLine("Enter Reason.");
                                    enhancement.reason = Console.ReadLine();
                                    Console.WriteLine("Enter Estimate.");
                                    enhancement.estimate = Console.ReadLine();
                                    enhancementFile.AddEnhancement(enhancement);
                                }
                                Console.WriteLine("Enter another enhancement (Y/N)?");
                                resp = Console.ReadLine().ToUpper();
                            } while (resp == "Y");
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine(enhancementFile.header);
                            foreach (Enhancement e in enhancementFile.Enhancements)
                            {
                                Console.WriteLine(e.Display());
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 5:
                        {
                            string resp = "Y";
                            do
                            {
                                if (resp == "Y")
                                {
                                    Task task = new Task();
                                    Console.WriteLine("Enter Summary.");
                                    task.summary = Console.ReadLine();
                                    Console.WriteLine("Enter Status.");
                                    task.status = Console.ReadLine();
                                    Console.WriteLine("Enter Priority.");
                                    task.priority = Console.ReadLine();
                                    Console.WriteLine("Enter Submitter.");
                                    task.submitter = Console.ReadLine();
                                    Console.WriteLine("Enter Assigned.");
                                    task.assigned = Console.ReadLine();
                                    Console.WriteLine("How many people are watching?");
                                    Int32.TryParse(Console.ReadLine(), out int watching);
                                    for (int i = 0; i < watching; i++)
                                    {
                                        Console.WriteLine($"Enter watcher {i + 1}");
                                        task.watching.Add(Console.ReadLine());
                                    }
                                    Console.WriteLine("Enter Project Name.");
                                    task.projectName = Console.ReadLine();
                                    Console.WriteLine("Enter Due Date.");
                                    task.dueDate = Console.ReadLine();
                                    taskFile.AddTask(task);
                                }
                                Console.WriteLine("Enter another task (Y/N)?");
                                resp = Console.ReadLine().ToUpper();
                            } while (resp == "Y");
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine(taskFile.header);
                            foreach (Task t in taskFile.Tasks)
                            {
                                Console.WriteLine(t.Display());
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Thank you for using this Ticket Application.");
                        break;
                }

            } while (answer >= 1 && answer <= 6);
            int answer2;
            do
            {
                Console.WriteLine("What would you like to search by?\n[1] Status\n[2] Priority\n[3] Submitter\nEnter to Quit.");
                int.TryParse(Console.ReadLine(), out answer2);
                switch (answer2)
                {
                    case 1:
                        string status;
                        Console.WriteLine("What is the Status you are searching for?");
                        status = Console.ReadLine();
                        var bugsWithStatus = bugFile.Bugs.Where(b => b.status.Contains(status));
                        var tasksWithStatus = taskFile.Tasks.Where(t => t.status.Contains(status));
                        var enhancementsWithStatus = enhancementFile.Enhancements.Where(e => e.status.Contains(status));
                        var allTicketStatus = (from x in bugsWithStatus select (Object)x).ToList();
                        allTicketStatus.AddRange((from x in tasksWithStatus select (Object)x).ToList());
                        allTicketStatus.AddRange((from x in enhancementsWithStatus select (Object)x).ToList());
                        Console.WriteLine($"\nThere are {allTicketStatus.Count()} tickets with {status} in their Status.\n");
                        foreach (Ticket t in allTicketStatus)
                        {
                            Console.WriteLine(t.Display());
                        }
                        break;
                    case 2:
                        string priority;
                        Console.WriteLine("What is the Priority you are searching for?");
                        priority = Console.ReadLine();
                        var bugsWithPriority = bugFile.Bugs.Where(b => b.priority.Contains(priority));
                        var tasksWithPriority = taskFile.Tasks.Where(t => t.priority.Contains(priority));
                        var enhancementsWithPriority = enhancementFile.Enhancements.Where(e => e.priority.Contains(priority));
                        var allTicketPriority = (from x in bugsWithPriority select (Object)x).ToList();
                        allTicketPriority.AddRange((from x in tasksWithPriority select (Object)x).ToList());
                        allTicketPriority.AddRange((from x in enhancementsWithPriority select (Object)x).ToList());
                        Console.WriteLine($"\nThere are {allTicketPriority.Count()} tickets with {priority} in their Priority.\n");
                        foreach (Ticket t in allTicketPriority)
                        {
                            Console.WriteLine(t.Display());
                        }
                        break;
                    case 3:
                        string submitter;
                        Console.WriteLine("What is the Submitter you are searching for?");
                        submitter = Console.ReadLine();
                        var bugsWithSubmitter = bugFile.Bugs.Where(b => b.submitter.Contains(submitter));
                        var tasksWithSubmitter = taskFile.Tasks.Where(t => t.submitter.Contains(submitter));
                        var enhancementsWithSubmitter = enhancementFile.Enhancements.Where(e => e.submitter.Contains(submitter));
                        var allTicketSubmitter = (from x in bugsWithSubmitter select (Object)x).ToList();
                        allTicketSubmitter.AddRange((from x in tasksWithSubmitter select (Object)x).ToList());
                        allTicketSubmitter.AddRange((from x in enhancementsWithSubmitter select (Object)x).ToList());
                        Console.WriteLine($"\nThere are {allTicketSubmitter.Count()} tickets with {submitter} in their Submitter.\n");
                        foreach (Ticket t in allTicketSubmitter)
                        {
                            Console.WriteLine(t.Display());
                        }
                        break;
                    default:
                        Console.WriteLine("Thank you for using this application.");
                        break;
                }
            } while (answer2 == 1 || answer2 == 2 || answer2 == 3);
            logger.Info("Program ended");
        }
    }
}