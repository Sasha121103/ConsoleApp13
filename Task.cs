using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class Task
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Deadline { get; set; }
        public required Employee Assignee { get; set; }
        public TaksStatus? Status { get; set; } = TaksStatus.Plannden; 
        public Risklevel? Risk { get; set; } = Risklevel.Gray;
        public List<int> SubTaskIds { get; set; } = new List<int>();
    }
}

//public enum Risk
//{
//    Gray, // 0
//    Red // 1

//}

//int enumValue = Risk.Gray;