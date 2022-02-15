using ShackUp.Data.Factory;
using ShackUp.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShackUp.UI.Controllers
{
    public class ListingsAPIController : ApiController
    {
        [Route("api/listing/search")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(decimal? minRate, decimal? maxRate, string city, string stateID)
        {
            var repo = ListingRepositoryFactory.GetRepository();

            try
            {
                var parameters = new ListingSearchParameters();

                parameters.MinRate = minRate;
                parameters.MaxRate = maxRate;
                parameters.City = city;
                parameters.StateID = stateID;


                var result = repo.Search(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/contact/check/{userID}/{ListingID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CheckContact(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                var result = repo.IsContact(userID, listingID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/favorite/check/{userID}/{ListingID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult CheckFavorite(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                var result = repo.IsFavorite(userID, listingID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/contact/add/{userID}/{listingID}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddContact(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                repo.AddContact(userID, listingID);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/contact/remove/{userID}/{listingID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult RemoveContact(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                repo.RemoveContact(userID, listingID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/favorite/add/{userID}/{listingID}")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddFavorite(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                repo.AddFavorites(userID, listingID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/favorite/remove/{userID}/{listingID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult RemoveFavorite(string userID, int listingID)
        {
            var repo = AccountRepositoryFactory.GetRepository();

            try
            {
                repo.RemoveFavorites(userID, listingID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
