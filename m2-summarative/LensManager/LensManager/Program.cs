using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensManager.Controllers;

namespace LensManager
{
    class Program
    {
        static void Main(string[] args)
        {
            LensController controller = new LensController();
            controller.Run();
        }
    }
}
