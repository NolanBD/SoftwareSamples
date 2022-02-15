using System;
using System.Collections.Generic;
using System.Text;

namespace ShackUp.Models.Queries
{
    public class ListingShortItem
    {
        public int ListingID { get; set; }
        public string UserID { get; set; }
        public decimal Rate { get; set; }
        public string StateID { get; set; }
        public string City { get; set; }
        public string ImageFileName { get; set; }
    }
}
