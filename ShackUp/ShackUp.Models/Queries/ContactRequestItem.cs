using System;
using System.Collections.Generic;
using System.Text;

namespace ShackUp.Models.Queries
{
    public class ContactRequestItem
    {
        public int ListingID { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string City { get; set; }
        public string StateID { get; set; }
        public decimal Rate { get; set; }
    }
}
