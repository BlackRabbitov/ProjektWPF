using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class Task
    {
        public string Name { get; set; }
        public int Importance { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<SubTask> SubTasks { get; set; }

        public List<Alert> Alerts;

        public Category Category;

        public bool IsDone;
        public Task() { }

        public Task(string name, int importance, Category category)
        {
            IsDone = false;
            Name = name;
            Importance = importance;
            Category = category;
            Alerts = new List<Alert>();
        }

        public Task(string name, int importance, DateTime startDate, DateTime endDate, Category category)
        {
            IsDone = false;
            Name = name;
            Importance = importance;
            StartDate = startDate;
            EndDate = endDate;
            Category = category;
            Alerts = new List<Alert>();
        }

        public Task(string name, int importance, DateTime startDate, Category category)
        {
            IsDone = false;
            Name = name;
            Importance = importance;
            StartDate = startDate;
            Category = category;
            Alerts = new List<Alert>();
        }
    }
}
