using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer.Interfaces
{
    public interface ITaxRepository
    {
        StateTax ReadByID(string stateAbbreviation);
        List<StateTax> ReadAll();
    }
}
