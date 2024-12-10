using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
     public class ReportGenerator
    {
        public List<Task> GetTasksByEmployee(List<Task> tasks, int employeeId) =>
            tasks.Where(t => t.Assignee.Id == employeeId).ToList();

        public List<(Task Task, int SubTaskCount)> GetTasksByCreationDateWithSubtasks(List<Task> tasks, DateTime start, DateTime end) =>
            tasks.Where(t => t.CreationDate >= start && t.CreationDate <= end)
                 .Select(t => (t, t.SubTaskIds.Count))
                 .ToList();

        public List<(Task Task, string EmployeeName)> GetTasksByStatusWithAssignees(List<Task> tasks, TaksStatus status) =>
            tasks.Where(t => t.Status==status)
                 .Select(t => (t, t.Assignee.Name))
                 .ToList();

        public List<(Task Task, int SubTaskCount)> GetTasksByRiskWithSubtasks(List<Task> tasks, Risklevel risk) =>
            tasks.Where(t=>t.Risk==risk)
                 .Select(t => (t, t.SubTaskIds.Count))
                 .ToList();

        public List<Task> GetSubTasks(List<Task> tasks, Task parentTask) =>
            tasks.Where(t => parentTask.SubTaskIds.Contains(t.Id)).ToList();
    }
}
