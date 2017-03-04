using System;
using System.Collections;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumeratorConsolApp
{
    class Lake:IEnumerable<Frog>
    {
        private Frog[] frogs;
        public Lake()
        {
            frogs = new Frog[] { new Frog("frog1"), new Frog("frog2"), new Frog("frog3"), new Frog("frog4") };
        }

        public IEnumerator<Frog> GetEnumerator()
        {
            return ((IEnumerable<Frog>)frogs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Frog>)frogs).GetEnumerator();
        }


    }
}
