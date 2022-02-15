using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdWebService.Models
{
    public class Dvd
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
        public string Notes { get; set; }
    }
}