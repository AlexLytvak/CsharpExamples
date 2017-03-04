using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventConsoleApp
{
    public delegate void Del();
    public  class HandlerArg
    {
        public int price;
        public  HandlerArg(int pr)
        {
            price = pr;
           
        }

        public event Del MyEvent;

        public void Alarm()
        {
            if (this.price > 0)
            {
                if (MyEvent != null)
                    MyEvent();
            }
        }


    }
}
