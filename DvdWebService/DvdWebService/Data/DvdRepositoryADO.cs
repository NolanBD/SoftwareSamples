using DvdWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DvdWebService.Data
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public void Create(Dvd newDVD)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@DvdId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@Notes", newDVD.Notes);
                cmd.Parameters.AddWithValue("@Title", newDVD.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", newDVD.ReleaseYear);
                cmd.Parameters.AddWithValue("@Rating", newDVD.Rating);
                cmd.Parameters.AddWithValue("@Director", newDVD.Director);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int dvdId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", dvdId);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public Dvd Get(int dvdId)
        {
            Dvd dvd = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //our procedure has a parameter in SQL, this is how we insert it.
                cmd.Parameters.AddWithValue("@DvdId", dvdId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        dvd = new Dvd();
                        dvd.id = (int)dr["DvdId"];
                        dvd.Notes = dr["Notes"].ToString();
                        dvd.Title = dr["Title"].ToString();
                        dvd.ReleaseYear = dr["ReleaseYear"].ToString();
                        dvd.Rating = dr["Rating"].ToString();
                        dvd.Director = dr["Director"].ToString();
                    }
                }
            }

            return dvd;
        }

        public IEnumerable<Dvd> GetAll()
        {
            List<Dvd> dvds = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = dr["ReleaseYear"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        dvds.Add(currentRow);
                    }
                }
            }

            return dvds;
        }

        public IEnumerable<Dvd> GetByDirector(string director)
        {
            List<Dvd> dvdsByDirector = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByDirector", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Director", director);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = dr["ReleaseYear"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        dvdsByDirector.Add(currentRow);
                    }
                }
            }

            return dvdsByDirector;
        }

        public IEnumerable<Dvd> GetByRating(string rating)
        {
            List<Dvd> dvdsByRating = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByRating", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Rating", rating);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = dr["ReleaseYear"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        dvdsByRating.Add(currentRow);
                    }
                }
            }

            return dvdsByRating;
        }

        public IEnumerable<Dvd> GetByReleaseYear(string releaseYear)
        {
            List<Dvd> dvdsByReleaseYear = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ReleaseYear", releaseYear);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = dr["ReleaseYear"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        dvdsByReleaseYear.Add(currentRow);
                    }
                }
            }

            return dvdsByReleaseYear;
        }

        public IEnumerable<Dvd> GetByTitle(string title)
        {
            List<Dvd> dvdsByTitle = new List<Dvd>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdSelectByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Title", title);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd();
                        currentRow.id = (int)dr["DvdId"];
                        currentRow.Notes = dr["Notes"].ToString();
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = dr["ReleaseYear"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Director = dr["Director"].ToString();
                        dvdsByTitle.Add(currentRow);
                    }
                }
            }

            return dvdsByTitle;
        }

        public void Update(Dvd updatedDVD)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DvdUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DvdId", updatedDVD.id);
                cmd.Parameters.AddWithValue("@Notes", updatedDVD.Notes);
                cmd.Parameters.AddWithValue("@Title", updatedDVD.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", updatedDVD.ReleaseYear);
                cmd.Parameters.AddWithValue("@Rating", updatedDVD.Rating);
                cmd.Parameters.AddWithValue("@Director", updatedDVD.Director);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}