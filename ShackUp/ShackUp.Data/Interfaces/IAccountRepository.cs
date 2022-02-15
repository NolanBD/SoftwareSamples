using ShackUp.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackUp.Data.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<FavoriteItem> GetFavorites(string userID);
        IEnumerable<ContactRequestItem> GetContacts(string userID);
        IEnumerable<ListingItem> GetListings(string userID);
        void AddFavorites(string userID, int listingID);
        void RemoveFavorites(string userID, int listingID);
        void AddContact(string userID, int listingID);
        void RemoveContact(string userID, int listingID);
        bool IsFavorite(string userID, int listingID);
        bool IsContact(string userID, int listingID);
    }
}
