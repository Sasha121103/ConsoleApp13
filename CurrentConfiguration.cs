using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public class CurrentConfiguration
    {
        public static Employee CurrentUser { get; private set; }

        public static void SetCurrentUser(Employee user) => CurrentUser = user;
    }

}
