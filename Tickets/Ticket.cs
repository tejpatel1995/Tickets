using System;
using System.Collections.Generic;
using System.Text;

namespace Tickets
{
    public abstract class Ticket
    {
        public int ticketID { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string submitter { get; set; }
        public string assigned { get; set; }
        public List<string> watching { get; set; }

        public Ticket()
        {
            watching = new List<string>();
        }

        public virtual string Display()
        {
            return $"{ticketID}, {summary}, {status}, {priority}, {submitter}, {assigned}, {string.Join("|", watching)}";
        }
    }

    public class Bug : Ticket
    {
        public string severity { get; set; }
        public override string Display()
        {
            return $"{base.Display()}, {severity}";
        }
    }

    public class Enhancement : Ticket
    {
        public string software { get; set; }
        public double cost { get; set; }
        public string reason { get; set; }
        public string estimate { get; set; }
        public override string Display()
        {
            return $"{base.Display()}, {software}, {cost}, {reason}, {estimate}";
        }
    }

    public class Task : Ticket
    {
        public string projectName { get; set; }
        public string dueDate { get; set; }
        public override string Display()
        {
            return $"{base.Display()}, {projectName}, {dueDate}";
        }
    }
}
