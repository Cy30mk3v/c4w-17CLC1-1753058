
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp2
{
    class PrintEvent
    {
        public delegate string Declare(string str);

        public event Declare Event;

        public PrintEvent()
        {
            this.Event += new Declare(this.HelloUser);
        }

        public string HelloUser(string user)
        {
            return "Hello " + user;
        }
    }
}
