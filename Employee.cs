using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
   
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public int? SupervisorId { get; set; }
    }

}
