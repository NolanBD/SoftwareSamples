using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensManager.Models
{
    public class Lenses
    {
        public int LensID { get; set; }
        public string LensName { get; set; }
        public int MinimumFocalLength { get; set; }
        public double MinimumAperture { get; set; }
        public bool IsCineLens { get; set; }
    }
}
