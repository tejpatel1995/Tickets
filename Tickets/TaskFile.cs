using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Tickets
{
    public class TaskFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List<Task> Tasks { get; set; }
        public string header { get; set; }
        public TaskFile(string path)
        {
            Tasks = new List<Task>();
            filePath = path;
            try
            {
                StreamReader sr = new StreamReader(filePath);
                header = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Task task = new Task();
                    string line = sr.ReadLine();
                    string[] arr = line.Split(',');
                    task.ticketID = int.Parse(arr[0]);
                    task.summary = arr[1];
                    task.status = arr[2];
                    task.priority = arr[3];
                    task.submitter = arr[4];
                    task.assigned = arr[5];
                    task.watching = arr[6].Split('|').ToList();
                    task.projectName = arr[7];
                    task.dueDate = arr[8];
                    Tasks.Add(task);
                }
                sr.Close();
                logger.Info("Tickets in file {Count}", Tasks.Count);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTask(Task task)
        {
            try
            {
                if (Tasks.Count == 0)
                    task.ticketID = 0;
                else
                    task.ticketID = Tasks.Max(t => t.ticketID) + 1;
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine($"{task.ticketID},{task.summary},{task.status},{task.priority}," +
                    $"{task.submitter},{task.assigned},{string.Join("|", task.watching)},{task.projectName},{task.dueDate}");
                sw.Close();
                Tasks.Add(task);
                logger.Info($"Ticket ID {task.ticketID} added");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
