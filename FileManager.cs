using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class FileManager
    {
        private string employeesFilePath;
        private string tasksFilePath;

        public FileManager(string employeesFilePath, string tasksFilePath)
        {
            this.employeesFilePath = employeesFilePath;
            this.tasksFilePath = tasksFilePath;
        }

        public List<Employee> LoadEmployees() =>
            JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText(employeesFilePath));

        public List<Task> LoadTasks() =>
            JsonConvert.DeserializeObject<List<Task>>(File.ReadAllText(tasksFilePath));

        public void SaveEmployees(List<Employee> employees) =>
            File.WriteAllText(employeesFilePath, JsonConvert.SerializeObject(employees, Newtonsoft.Json.Formatting.Indented));

        public void SaveTasks(List<Task> tasks) =>
            File.WriteAllText(tasksFilePath, JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented));
    }

}
