using ShackUp.Data.Interfaces;
using ShackUp.Models.Queries;
using ShackUp.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackUp.Data.ADO
{
    public class ListingsRepositoryADO : IListingsRepository
    {
        public void Delete(int listingID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ListingID", listingID);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public Listing GetByID(int listingID)
        {
            Listing listing = null;
            
            List<Listing> bathroomTypes = new List<Listing>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //our procedure has a parameter in SQL, this is how we insert it.
                cmd.Parameters.AddWithValue("@ListingID", listingID);
                
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        listing = new Listing();
                        listing.ListingID = (int)dr["ListingID"];
                        listing.UserID = dr["UserID"].ToString();
                        listing.StateID = dr["StateID"].ToString();
                        listing.BathroomTypeID = (int)dr["BathroomTypeID"];
                        listing.Nickname = dr["Nickname"].ToString();
                        listing.City = dr["City"].ToString();
                        listing.Rate = (decimal)dr["Rate"];
                        listing.SquareFootage = (decimal)dr["SquareFootage"];
                        listing.HasElectric = (bool)dr["HasElectric"];
                        listing.HasHeat = (bool)dr["HasHeat"];

                        if (dr["ListingDescription"] != DBNull.Value)
                            listing.ListingDescription = dr["ListingDescription"].ToString();

                        if(dr["ImageFileName"] != DBNull.Value)
                            listing.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return listing;
        }

        public ListingItem GetDetails(int listingID)
        {
            ListingItem listing = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //our procedure has a parameter in SQL, this is how we insert it.
                cmd.Parameters.AddWithValue("@ListingID", listingID);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        listing = new ListingItem();
                        listing.ListingID = (int)dr["ListingID"];
                        listing.UserID = dr["UserID"].ToString();
                        listing.StateID = dr["StateID"].ToString();
                        listing.BathroomTypeID = (int)dr["BathroomTypeID"];
                        listing.Nickname = dr["Nickname"].ToString();
                        listing.City = dr["City"].ToString();
                        listing.Rate = (decimal)dr["Rate"];
                        listing.SquareFootage = (decimal)dr["SquareFootage"];
                        listing.HasElectric = (bool)dr["HasElectric"];
                        listing.HasHeat = (bool)dr["HasHeat"];
                        listing.BathroomTypeName = dr["BathroomTypeName"].ToString();

                        if (dr["ListingDescription"] != DBNull.Value)
                        {
                            listing.ListingDescription = dr["ListingDescription"].ToString();
                        }

                        if (dr["ImageFileName"] != DBNull.Value)
                            listing.ImageFileName = dr["ImageFileName"].ToString();
                    }
                }
            }

            return listing;
        }

        public IEnumerable<ListingShortItem> GetRecent()
        {
            List<ListingShortItem> listings = new List<ListingShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingSelectRecent", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListingShortItem row = new ListingShortItem();
                        row.ListingID = (int)dr["ListingID"];
                        row.UserID = dr["UserID"].ToString();
                        row.StateID = dr["StateID"].ToString();
                        row.City = dr["City"].ToString();
                        row.Rate = (decimal)dr["Rate"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        listings.Add(row);
                    }
                }
            }

            return listings;
        }

        public void Insert(Listing listing)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ListingID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                //our procedure has a parameter in SQL, this is how we insert it.
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@UserID", listing.UserID);
                cmd.Parameters.AddWithValue("@StateID", listing.StateID);
                cmd.Parameters.AddWithValue("@BathroomTypeID", listing.BathroomTypeID);
                cmd.Parameters.AddWithValue("@City", listing.City);
                cmd.Parameters.AddWithValue("@Nickname", listing.Nickname);
                cmd.Parameters.AddWithValue("@Rate", listing.Rate);
                cmd.Parameters.AddWithValue("@SquareFootage", listing.SquareFootage);
                cmd.Parameters.AddWithValue("@HasElectric", listing.HasElectric);
                cmd.Parameters.AddWithValue("@HasHeat", listing.HasHeat);
                
                if (string.IsNullOrEmpty(listing.ImageFileName))
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileName", listing.ImageFileName);
                }

                cmd.Parameters.AddWithValue("@ListingDescription", listing.ListingDescription);
                cn.Open();

                cmd.ExecuteNonQuery();

                listing.ListingID = (int)param.Value;
            }
        }

        public IEnumerable<ListingShortItem> Search(ListingSearchParameters parameters)
        {
            List<ListingShortItem> listings = new List<ListingShortItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 12 ListingID, UserID, City, StateID, Rate, ImageFileName FROM Listings WHERE 1 = 1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.MinRate.HasValue)
                {
                    query += " AND Rate >= @MinRate";
                    cmd.Parameters.AddWithValue("@MinRate", parameters.MinRate.Value);
                }

                if (parameters.MaxRate.HasValue)
                {
                    query += " AND Rate <= @MaxRate";
                    cmd.Parameters.AddWithValue("@MaxRate", parameters.MaxRate.Value);
                }

                if (!string.IsNullOrEmpty(parameters.City))
                {
                    query += " AND City LIKE @City";
                    cmd.Parameters.AddWithValue("@City", parameters.City + "%");
                }

                if (!string.IsNullOrEmpty(parameters.StateID))
                {
                    query += " AND StateID = @StateID";
                    cmd.Parameters.AddWithValue("@StateID", parameters.StateID);
                }

                query += " ORDER BY CreatedDate DESC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListingShortItem row = new ListingShortItem();
                        row.ListingID = (int)dr["ListingID"];
                        row.UserID = dr["UserID"].ToString();
                        row.StateID = dr["StateID"].ToString();
                        row.City = dr["City"].ToString();
                        row.Rate = (decimal)dr["Rate"];

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.ImageFileName = dr["ImageFileName"].ToString();

                        listings.Add(row);
                    }
                }
            }

            return listings;
        }

        public void Update(Listing listing)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ListingID", listing.ListingID);
                cmd.Parameters.AddWithValue("@UserID", listing.UserID);
                cmd.Parameters.AddWithValue("@StateID", listing.StateID);
                cmd.Parameters.AddWithValue("@BathroomTypeID", listing.BathroomTypeID);
                cmd.Parameters.AddWithValue("@City", listing.City);
                cmd.Parameters.AddWithValue("@Nickname", listing.Nickname);
                cmd.Parameters.AddWithValue("@Rate", listing.Rate);
                cmd.Parameters.AddWithValue("@SquareFootage", listing.SquareFootage);
                cmd.Parameters.AddWithValue("@HasElectric", listing.HasElectric);
                cmd.Parameters.AddWithValue("@HasHeat", listing.HasHeat);
                cmd.Parameters.AddWithValue("@ImageFileName", listing.ImageFileName);
                cmd.Parameters.AddWithValue("@ListingDescription", listing.ListingDescription);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
