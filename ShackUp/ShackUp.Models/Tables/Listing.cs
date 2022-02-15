using System;
using System.Collections.Generic;
using System.Text;

namespace ShackUp.Models.Tables
{
    public class Listing
    {
        public int ListingID { get; set; }
        public string UserID { get; set; }
        public string StateID { get; set; }
        public int BathroomTypeID { get; set; }
        public string Nickname { get; set; }
        public string City { get; set; }
        public decimal Rate { get; set; }
        public decimal SquareFootage { get; set; }
        public bool HasElectric { get; set; }
        public bool HasHeat { get; set; }
        public string ImageFileName { get; set; }
        public string ListingDescription { get; set; }
    }
}
