using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace FlooringMastery
{
    class Program
    {
        static void Main(string[] args)
        {
            ControllerClient controller = new ControllerClient();
            controller.Run();
        }
    }
}
