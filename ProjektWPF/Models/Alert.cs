using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class Alert
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }

        public Alert(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;
        }
        public Alert()
        {

        }
    }
}
