using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<SubTask> SubTasks;

        public Category _Category;

        public bool IsDone;

        public Task(string name, int importance)
        {
            IsDone = false;
            Name = name;
            Importance = importance;
        }
    }
}
