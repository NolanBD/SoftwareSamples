﻿using NUnit.Framework;
using ShackUp.Data.ADO;
using ShackUp.Models.Queries;
using ShackUp.Models.Tables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackUp.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Init()
        {
            using(var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryADO();

            var states = repo.GetAll();

            Assert.AreEqual(3, states.Count);

            Assert.AreEqual("KY", states[0].StateID);
            Assert.AreEqual("Kentucky", states[0].StateName);
        }

        [Test]
        public void CanLoadBathroomTypes()
        {
            var repo = new BathroomTypesRepositoryADO();
            var types = repo.GetAll();

            Assert.AreEqual(3, types.Count);

            Assert.AreEqual(1, types[0].BathroomTypeID);
            Assert.AreEqual("Indoor", types[0].BathroomTypeName);
        }

        [Test]
        public void CanLoadListing()
        {
            var repo = new ListingsRepositoryADO();
            var listing = repo.GetByID(1);

            Assert.IsNotNull(listing);
            //1, '00000000-0000-0000-0000-000000000000', 'OH', 3, 'Test Shack 1', 'Cleveland', 120, 400, 0, 1, 'placeholder.png'
            Assert.AreEqual(1, listing.ListingID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", listing.UserID);
            Assert.AreEqual("OH", listing.StateID);
            Assert.AreEqual(3, listing.BathroomTypeID);
            Assert.AreEqual("Test Shack 1", listing.Nickname);
            Assert.AreEqual("Cleveland", listing.City);
            Assert.AreEqual(100M, listing.Rate);
            Assert.AreEqual(400M, listing.SquareFootage);
            Assert.AreEqual(false, listing.HasElectric);
            Assert.AreEqual(true, listing.HasHeat);
            Assert.AreEqual("placeholder.png", listing.ImageFileName);
            Assert.AreEqual("DESCRIPTION", listing.ListingDescription);
        }

        [Test]
        public void NotFoundListingReturnsNull()
        {
            var repo = new ListingsRepositoryADO();
            var listing = repo.GetByID(100000);

            Assert.IsNull(listing);
        }


        [Test]
        public void CanAddListing()
        {
            Listing listingToAdd = new Listing();
            var repo = new ListingsRepositoryADO();

            listingToAdd.UserID = "00000000-0000-0000-0000-000000000000";
            listingToAdd.StateID = "OH";
            listingToAdd.BathroomTypeID = 1;
            listingToAdd.City = "Columbus";
            listingToAdd.Rate = 50M;
            listingToAdd.SquareFootage = 100M;
            listingToAdd.HasElectric = true;
            listingToAdd.HasHeat = true;
            listingToAdd.ImageFileName = "placeholder.png";
            listingToAdd.Nickname = "My New Shack";
            listingToAdd.ListingDescription = "Description";
            repo.Insert(listingToAdd);

            Assert.AreEqual(7, listingToAdd.ListingID);
        }

        [Test]
        public void CanUpdateListing()
        {
            Listing listingToAdd = new Listing();
            var repo = new ListingsRepositoryADO();

            listingToAdd.UserID = "00000000-0000-0000-0000-000000000000";
            listingToAdd.StateID = "OH";
            listingToAdd.BathroomTypeID = 1;
            listingToAdd.City = "Columbus";
            listingToAdd.Rate = 50M;
            listingToAdd.SquareFootage = 100M;
            listingToAdd.HasElectric = true;
            listingToAdd.HasHeat = true;
            listingToAdd.ImageFileName = "placeholder.png";
            listingToAdd.Nickname = "My New Shack";
            listingToAdd.ListingDescription = "Description";

            repo.Insert(listingToAdd);

            listingToAdd.StateID = "KY";
            listingToAdd.BathroomTypeID = 2;
            listingToAdd.City = "Louisville";
            listingToAdd.Rate = 25M;
            listingToAdd.SquareFootage = 75M;
            listingToAdd.HasElectric = false;
            listingToAdd.HasHeat = false;
            listingToAdd.ImageFileName = "update.png";
            listingToAdd.Nickname = "My Updated Shack";
            listingToAdd.ListingDescription = "Updated";

            repo.Update(listingToAdd);

            var updatedListing = repo.GetByID(7);

            Assert.AreEqual("KY", updatedListing.StateID);
            Assert.AreEqual("My Updated Shack", updatedListing.Nickname);
            Assert.AreEqual(2, updatedListing.BathroomTypeID);
            Assert.AreEqual("Louisville", updatedListing.City);
            Assert.AreEqual(25M, updatedListing.Rate);
            Assert.AreEqual(75M, updatedListing.SquareFootage);
            Assert.AreEqual(false, updatedListing.HasElectric);
            Assert.AreEqual(false, updatedListing.HasHeat);
            Assert.AreEqual("update.png", updatedListing.ImageFileName);
            Assert.AreEqual("Updated", updatedListing.ListingDescription);
        }

        [Test]
        public void CanDeleteListing()
        {
            Listing listingToAdd = new Listing();
            var repo = new ListingsRepositoryADO();

            listingToAdd.UserID = "00000000-0000-0000-0000-000000000000";
            listingToAdd.StateID = "OH";
            listingToAdd.BathroomTypeID = 1;
            listingToAdd.City = "Columbus";
            listingToAdd.Rate = 50M;
            listingToAdd.SquareFootage = 100M;
            listingToAdd.HasElectric = true;
            listingToAdd.HasHeat = true;
            listingToAdd.ImageFileName = "placeholder.png";
            listingToAdd.Nickname = "My New Shack";
            listingToAdd.ListingDescription = "Description";

            repo.Insert(listingToAdd);

            var loaded = repo.GetByID(7);
            Assert.IsNotNull(loaded);

            repo.Delete(7);
            loaded = repo.GetByID(7);
            Assert.IsNull(loaded);
        }

        [Test]
        public void CanLoadRecentListings()
        {
            var repo = new ListingsRepositoryADO();
            List<ListingShortItem> listings = repo.GetRecent().ToList();

            Assert.AreEqual(5, listings.Count());

            Assert.AreEqual(6, listings[0].ListingID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", listings[0].UserID);
            Assert.AreEqual(150M, listings[0].Rate);
            Assert.AreEqual("Cleveland", listings[0].City);
            Assert.AreEqual("OH", listings[0].StateID);
            Assert.AreEqual("placeholder.png", listings[0].ImageFileName);
        }

        [Test]
        public void CanLoadListingDetails()
        {
            var repo = new ListingsRepositoryADO();
            var listing = repo.GetDetails(1);

            Assert.IsNotNull(listing);
            //1, '00000000-0000-0000-0000-000000000000', 'OH', 3, 'Test Shack 1', 'Cleveland', 120, 400, 0, 1, 'placeholder.png'
            Assert.AreEqual(1, listing.ListingID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", listing.UserID);
            Assert.AreEqual("OH", listing.StateID);
            Assert.AreEqual(3, listing.BathroomTypeID);
            Assert.AreEqual("Test Shack 1", listing.Nickname);
            Assert.AreEqual("Cleveland", listing.City);
            Assert.AreEqual(100M, listing.Rate);
            Assert.AreEqual(400M, listing.SquareFootage);
            Assert.AreEqual(false, listing.HasElectric);
            Assert.AreEqual(true, listing.HasHeat);
            Assert.AreEqual("placeholder.png", listing.ImageFileName);
            Assert.AreEqual("None", listing.BathroomTypeName);
            Assert.AreEqual("DESCRIPTION", listing.ListingDescription);
        }

        [Test]
        public void CanLoadFavorites()
        {
            var repo = new AccountRepositoryADO();
            var favorites = repo.GetFavorites("11111111-1111-1111-1111-111111111111").ToList();

            Assert.AreEqual(2, favorites.Count());

            Assert.AreEqual(1, favorites[0].ListingID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", favorites[0].UserID);
            Assert.AreEqual("OH", favorites[0].StateID);
            Assert.AreEqual("Cleveland", favorites[0].City);
            Assert.AreEqual(100M, favorites[0].Rate);
            Assert.AreEqual(400M, favorites[0].SquareFootage);
            Assert.AreEqual(false, favorites[0].HasElectric);
            Assert.AreEqual(true, favorites[0].HasHeat);
            Assert.AreEqual("None", favorites[0].BathroomTypeName);
            Assert.AreEqual(3, favorites[0].BathroomTypeID);
        }

        [Test]
        public void CanGetContacts()
        {
            var repo = new AccountRepositoryADO();
            var contacts = repo.GetContacts("00000000-0000-0000-0000-000000000000").ToList();

            Assert.AreEqual(2, contacts.Count());

            Assert.AreEqual(1, contacts[0].ListingID);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", contacts[0].UserID);
            Assert.AreEqual("OH", contacts[0].StateID);
            Assert.AreEqual("Cleveland", contacts[0].City);
            Assert.AreEqual(100M, contacts[0].Rate);
            Assert.AreEqual("test2@test.com", contacts[0].Email);
            Assert.AreEqual("Test Shack 1", contacts[0].Nickname);
        }

        [Test]
        public void CanLoadListingsForUser()
        {
            var repo = new AccountRepositoryADO();
            var listings = repo.GetListings("00000000-0000-0000-0000-000000000000").ToList();

            Assert.AreEqual(6, listings.Count());

            Assert.AreEqual(1, listings[0].ListingID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", listings[0].UserID);
            Assert.AreEqual("OH", listings[0].StateID);
            Assert.AreEqual(3, listings[0].BathroomTypeID);
            Assert.AreEqual("Test Shack 1", listings[0].Nickname);
            Assert.AreEqual("Cleveland", listings[0].City);
            Assert.AreEqual(100M, listings[0].Rate);
            Assert.AreEqual(400M, listings[0].SquareFootage);
            Assert.AreEqual(false, listings[0].HasElectric);
            Assert.AreEqual(true, listings[0].HasHeat);
            Assert.AreEqual("placeholder.png", listings[0].ImageFileName);
            Assert.AreEqual("None", listings[0].BathroomTypeName);
        }

        [Test]
        public void CanAddAndRemoveFavorites()
        {
            var repo = new AccountRepositoryADO();

            repo.AddFavorites("11111111-1111-1111-1111-111111111111", 3);

            var favorites = repo.GetFavorites("11111111-1111-1111-1111-111111111111");

            Assert.AreEqual(3, favorites.Count());

            repo.RemoveFavorites("11111111-1111-1111-1111-111111111111", 3);

            favorites = repo.GetFavorites("11111111-1111-1111-1111-111111111111");
            Assert.AreEqual(2, favorites.Count());
        }

        [Test]
        public void CanAddAndRemoveContacts()
        {
            var repo = new AccountRepositoryADO();

            repo.AddContact("11111111-1111-1111-1111-111111111111", 5);

            var contacts = repo.GetContacts("00000000-0000-0000-0000-000000000000");

            Assert.AreEqual(3, contacts.Count());

            repo.RemoveContact("11111111-1111-1111-1111-111111111111", 3);

            contacts = repo.GetContacts("00000000-0000-0000-0000-000000000000");
            Assert.AreEqual(2, contacts.Count());
        }

        [Test]
        public void CanDetectFavorite()
        {
            var repo = new AccountRepositoryADO();

            var found = repo.IsFavorite("11111111-1111-1111-1111-111111111111", 2);

            Assert.IsTrue(found);

            found = repo.IsFavorite("11111111-1111-1111-1111-111111111111", 10);
            Assert.IsFalse(found);
        }

        [Test]
        public void CanDetectContact()
        {
            var repo = new AccountRepositoryADO();

            var found = repo.IsContact("11111111-1111-1111-1111-111111111111", 1);

            Assert.IsTrue(found);

            found = repo.IsContact("11111111-1111-1111-1111-111111111111", 10);
            Assert.IsFalse(found);
        }

        [Test]
        public void CanSearchOnRate()
        {
            var repo = new ListingsRepositoryADO();

            var found = repo.Search(new ListingSearchParameters { MinRate = 110M });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchOnMaxRate()
        {
            var repo = new ListingsRepositoryADO();

            var found = repo.Search(new ListingSearchParameters { MaxRate = 110M });

            Assert.AreEqual(2, found.Count());
        }

        [Test]
        public void CanSearchOnMinRate()
        {
            var repo = new ListingsRepositoryADO();

            var found = repo.Search(new ListingSearchParameters { MinRate = 110M });

            Assert.AreEqual(5, found.Count());
        }

        [Test]
        public void CanSearchOnRateRange()
        {
            var repo = new ListingsRepositoryADO();

            var found = repo.Search(new ListingSearchParameters { MaxRate = 120M, MinRate = 100M });

            Assert.AreEqual(3, found.Count());
        }

        [Test]
        public void CanSearchOnCity()
        {
            var repo = new ListingsRepositoryADO();

            var found = repo.Search(new ListingSearchParameters { City = "Col" });

            Assert.AreEqual(1, found.Count());
        }
    }
}
