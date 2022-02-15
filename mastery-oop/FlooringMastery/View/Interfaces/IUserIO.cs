using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Interfaces
{
    public interface IUserIO
    {
        string ReadString(string prompt);
        StateTax ReadState(List<StateTax> list);
        Product ReadProduct(List<Product> list);
        int ReadInt(string prompt);
        decimal ReadDecimal(string prompt, decimal min);
        DateTime ReadDate(string prompt);
        DateTime ReadDateAfterCurrentDate(string prompt);
        bool ReadBool(string prompt);
        void DisplayMessage(string message);
    }
}
