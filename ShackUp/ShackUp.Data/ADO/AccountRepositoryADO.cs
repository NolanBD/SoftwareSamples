using ShackUp.Data.Interfaces;
using ShackUp.Models.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackUp.Data.ADO
{
    public class AccountRepositoryADO : IAccountRepository
    {
        public void AddContact(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void AddFavorites(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FavoriteInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<ContactRequestItem> GetContacts(string userID)
        {
            List<ContactRequestItem> listings = new List<ContactRequestItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelectContacts", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ContactRequestItem row = new ContactRequestItem();
                        row.ListingID = (int)dr["ListingID"];
                        row.UserID = dr["UserID"].ToString();
                        row.StateID = dr["StateID"].ToString();
                        row.City = dr["City"].ToString();
                        row.Rate = (decimal)dr["Rate"];
                        row.Email = dr["Email"].ToString();
                        row.Nickname = dr["Nickname"].ToString();

                        listings.Add(row);
                    }
                }
            }

            return listings;
        }

        public IEnumerable<FavoriteItem> GetFavorites(string userID)
        {
            List<FavoriteItem> listings = new List<FavoriteItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelectFavorites", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FavoriteItem row = new FavoriteItem();
                        row.ListingID = (int)dr["ListingID"];
                        row.UserID = dr["UserID"].ToString();
                        row.StateID = dr["StateID"].ToString();
                        row.City = dr["City"].ToString();
                        row.Rate = (decimal)dr["Rate"];
                        row.BathroomTypeID = (int)dr["BathroomTypeID"];
                        row.BathroomTypeName = dr["BathroomTypeName"].ToString();
                        row.SquareFootage = (decimal)dr["SquareFootage"];
                        row.HasElectric = (bool)dr["HasElectric"];
                        row.HasHeat = (bool)dr["HasHeat"];

                        listings.Add(row);
                    }
                }
            }

            return listings;
        }

        public IEnumerable<ListingItem> GetListings(string userID)
        {
            List<ListingItem> listings = new List<ListingItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelectByUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListingItem row = new ListingItem();
                        row.ListingID = (int)dr["ListingID"];
                        row.UserID = dr["UserID"].ToString();
                        row.Nickname = dr["Nickname"].ToString();
                        row.StateID = dr["StateID"].ToString();
                        row.City = dr["City"].ToString();
                        row.SquareFootage = (decimal)dr["SquareFootage"];
                        row.Rate = (decimal)dr["Rate"];
                        row.HasElectric = (bool)dr["HasElectric"];
                        row.HasHeat = (bool)dr["HasHeat"];
                        row.BathroomTypeName = dr["BathroomTypeName"].ToString();
                        row.BathroomTypeID = (int)dr["BathroomTypeID"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        listings.Add(row);
                    }
                }
            }

            return listings;
        }

        public bool IsContact(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                using(var dr = cmd.ExecuteReader())
                {
                    return dr.Read();
                }
            }
        }

        public bool IsFavorite(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FavoriteSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    return dr.Read();
                }
            }
        }

        public void RemoveContact(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveFavorites(string userID, int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FavoriteDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
