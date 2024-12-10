using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{

    public class TaskManager
    {
        public void AddTask(List<Task> tasks, Task newTask) => tasks.Add(newTask);
    }
}