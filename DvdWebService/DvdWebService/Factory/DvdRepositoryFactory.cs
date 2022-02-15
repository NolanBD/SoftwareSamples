using DvdWebService.Data;
using DvdWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdWebService.Factory
{
    public class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "ADO":
                    return new DvdRepositoryADO();
                case "SampleData":
                    return new DvdRepositoryMock();
                default:
                    throw new Exception("Could not find valid 'Mode' configuration value.");
            }
        }
    }
}