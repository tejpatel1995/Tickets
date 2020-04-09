using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Tickets
{
    public class BugFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List<Bug> Bugs { get; set; }
        public string header { get; set; }
        public BugFile(string path)
        {
            Bugs = new List<Bug>();
            filePath = path;
            try
            {
                StreamReader sr = new StreamReader(filePath);
                header = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Bug bug = new Bug();
                    string line = sr.ReadLine();
                    string[] arr = line.Split(',');
                    bug.ticketID = int.Parse(arr[0]);
                    bug.summary = arr[1];
                    bug.status = arr[2];
                    bug.priority = arr[3];
                    bug.submitter = arr[4];
                    bug.assigned = arr[5];
                    bug.watching = arr[6].Split('|').ToList();
                    bug.severity = arr[7];
                    Bugs.Add(bug);
                }
                sr.Close();
                logger.Info("Tickets in file {Count}", Bugs.Count);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddBug(Bug bug)
        {
            try
            {
                if (Bugs.Count == 0)
                    bug.ticketID = 0;
                else
                    bug.ticketID = Bugs.Max(b => b.ticketID) + 1;
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine($"{bug.ticketID},{bug.summary},{bug.status},{bug.priority}," +
                    $"{bug.submitter},{bug.assigned},{string.Join("|", bug.watching)},{bug.severity}");
                sw.Close();
                Bugs.Add(bug);
                logger.Info($"Ticket ID {bug.ticketID} added");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
