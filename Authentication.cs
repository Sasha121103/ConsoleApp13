using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class Authentication
    {
        private List<Employee> employees;

        public Authentication(List<Employee> employees) => this.employees = employees;

        public Employee Login(string login, string password)
        {
            var user = employees.FirstOrDefault(e => e.Login == login && e.Password == password);
            if (user == null) throw new UnauthorizedAccessException("Неверный логин или пароль.");
            return user;
        }
    }
}
