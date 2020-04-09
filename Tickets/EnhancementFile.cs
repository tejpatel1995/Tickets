using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Tickets
{
    public class EnhancementFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public string filePath { get; set; }
        public List<Enhancement> Enhancements { get; set; }
        public string header { get; set; }
        public EnhancementFile(string path)
        {
            Enhancements = new List<Enhancement>();
            filePath = path;
            try
            {
                StreamReader sr = new StreamReader(filePath);

                header = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    Enhancement enhancement = new Enhancement();
                    string line = sr.ReadLine();
                    string[] arr = line.Split(',');
                    enhancement.ticketID = int.Parse(arr[0]);
                    enhancement.summary = arr[1];
                    enhancement.status = arr[2];
                    enhancement.priority = arr[3];
                    enhancement.submitter = arr[4];
                    enhancement.assigned = arr[5];
                    enhancement.watching = arr[6].Split('|').ToList();
                    enhancement.software = arr[7];
                    enhancement.cost = double.Parse(arr[8]);
                    enhancement.reason = arr[9];
                    enhancement.estimate = arr[10];
                    Enhancements.Add(enhancement);
                }
                sr.Close();
                logger.Info("Tickets in file {Count}", Enhancements.Count);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddEnhancement(Enhancement enhancement)
        {
            try
            {
                if (Enhancements.Count == 0)
                    enhancement.ticketID = 0;
                else
                    enhancement.ticketID = Enhancements.Max(e => e.ticketID) + 1;
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine($"{enhancement.ticketID},{enhancement.summary},{enhancement.status},{enhancement.priority}," +
                    $"{enhancement.submitter},{enhancement.assigned},{string.Join("|", enhancement.watching)},{enhancement.software},{enhancement.cost},{enhancement.reason},{enhancement.estimate}");
                sw.Close();
                Enhancements.Add(enhancement);
                logger.Info($"Ticket ID {enhancement.ticketID} added");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
