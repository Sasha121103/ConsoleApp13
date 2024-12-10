using System.Xml;
using ConsoleApp13;
using Newtonsoft.Json;


class Program
{
    static void Main(string[] args)
    {
        string employeesFile = "People.txt";
        string tasksFile = "Tasks.txt";

        FileManager fileManager = new FileManager(employeesFile, tasksFile);
        var employees = fileManager.LoadEmployees();
        var tasks = fileManager.LoadTasks();
        Authentication auth = new Authentication(employees);

        Console.WriteLine("Введите логин:");
        string login = Console.ReadLine();

        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();

        try
        {
            var user = auth.Login(login, password);
            CurrentConfiguration.SetCurrentUser(user);
            Console.WriteLine($"Добро пожаловать, {user.Name}!");

            ReportGenerator reportGenerator = new ReportGenerator();

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Показать задачи конкретного сотрудника");
                Console.WriteLine("2. Показать задачи за определённое время");
                Console.WriteLine("3. Показать задачи с определённым статусом");
                Console.WriteLine("4. Показать задачи с определённым риском");
                Console.WriteLine("5. Показать все подзадачи конкретной задачи");
                Console.WriteLine("0. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите ID сотрудника:");
                        int employeeId = int.Parse(Console.ReadLine());
                        var employeeTasks = reportGenerator.GetTasksByEmployee(tasks, employeeId);
                        foreach (var task in employeeTasks)
                            Console.WriteLine($"ID: {task.Id}, Название: {task.Title}, Статус: {task.Status}, Дедлайн: {task.Deadline}");
                        break;

                    case "2":
                        Console.WriteLine("Введите начальную дату (ГГГГ-ММ-ДД):");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введите конечную дату (ГГГГ-ММ-ДД):");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());
                        var tasksByDate = reportGenerator.GetTasksByCreationDateWithSubtasks(tasks, startDate, endDate);
                        foreach (var (task, count) in tasksByDate)
                            Console.WriteLine($"Название: {task.Title}, Подзадач: {count}, Статус: {task.Status}");
                        break;

                    case "3":
                        Console.WriteLine("Введите статус задач(0-Запланированно,1-в процессе,2- Завершена):");
                        string statusLeveInput = Console.ReadLine();
                        if(!Enum.TryParse(statusLeveInput, out TaksStatus selectedStatus) || !Enum.IsDefined(typeof (TaksStatus), selectedStatus)){
                            Console.WriteLine("Некоррентный  статус задачи.");
                            break;
                        }
                        var tasksByStatus = reportGenerator.GetTasksByStatusWithAssignees(tasks, selectedStatus);
                        foreach (var (task, employeeName) in tasksByStatus)
                            Console.WriteLine($"Название: {task.Title}, Сотрудник: {employeeName}, Дедлайн: {task.Deadline} Статус:{task.Status} ");
                        break;

                    case "4":
                        Console.WriteLine("Введите уровень риска( Gray,Green,Yellow,Red):");
                        string riskLeveInput = Console.ReadLine();
                        if(!Enum.TryParse(riskLeveInput,true,out Risklevel selectedRisk))
                        {
                            Console.WriteLine("Некоррентное уровень риска");
                            break;
                        }
                        var tasksByRisk = reportGenerator.GetTasksByRiskWithSubtasks(tasks, selectedRisk);
                        foreach (var (task, count) in tasksByRisk)
                            Console.WriteLine($"Название: {task.Title}, Подзадач: {count}, Уровень риска:{task.Risk}");
                        break;

                    case "5":
                        Console.WriteLine("Введите ID родительской задачи:");

                        if (!int.TryParse(Console.ReadLine(), out int  parentTaksId)){
                            Console.WriteLine("Некоррентное Id задачи");
                        }
                        var parentTask=tasks.FirstOrDefault(t=>t.Id == parentTaksId);
                        if (parentTask == null)
                        {
                            Console.WriteLine("Задача не найдена.");
                            break;
                        }
                        var subTaks=tasks.Where(t=>parentTask.SubTaskIds.Contains(t.Id)).ToList();
                        if (!subTaks.Any())
                        {
                            Console.WriteLine(" У данной задачи нет подзадач!");
                        }
                        else
                        {
                            Console.WriteLine($"Подзадачи для задачи \"{parentTask.Title}\":");
                        }foreach (var subTask in subTaks)
                        {

                            Console.WriteLine($"ID: {subTask.Id}, Название: {subTask.Title}, Дедлайн: {subTask.Deadline.ToShortDateString}, Статус: {subTask.Status}");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Выход из программы.");
                        return;

                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова.");
                        break;
                }
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
