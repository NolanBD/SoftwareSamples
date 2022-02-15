using ShackUp.Models.Queries;
using ShackUp.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackUp.Data.Interfaces
{
    public interface IListingsRepository
    {
        Listing GetByID(int listingID);
        void Insert(Listing listing);
        void Update(Listing listing);
        void Delete(int listingID);
        IEnumerable<ListingShortItem> GetRecent();
        ListingItem GetDetails(int listingID);
        IEnumerable<ListingShortItem> Search(ListingSearchParameters parameters);
    }
}
