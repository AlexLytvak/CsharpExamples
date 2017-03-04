using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventConsoleApp
{
    public delegate void MyEventDelegate();
    public  class Shop
    {
        public  event MyEventDelegate SeampleEvent;
        public string msg { get; set; }

        public Shop(string Msg)
        {
            msg = Msg;
            SeampleEvent=()=>{ };
            
        }

        public void ActivEvent()
        {
            SeampleEvent();
        }
    }
}
