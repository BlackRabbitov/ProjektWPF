using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class SubTask
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SubTask(string name)
        {
            Name = name;
        }
        public SubTask(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public SubTask(string name, DateTime startDate)
        {
            Name = name;
            StartDate = startDate;
        }

        public SubTask()
        {
        }
    }
}
