using DvdWebService.Factory;
using DvdWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdWebService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetAll());
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            Dvd found = repo.Get(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [Route("dvd/")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(Dvd newDvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Create(newDvd);

            return Created($"dvd/{newDvd.id}", newDvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, Dvd updatedDvd)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Update(updatedDvd);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            repo.Delete(id);
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string title)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetByTitle(title));
        }

        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByReleaseYear(string releaseYear)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetByReleaseYear(releaseYear));
        }

        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string rating)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetByRating(rating));
        }

        [Route("dvds/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string director)
        {
            var repo = DvdRepositoryFactory.GetRepository();

            return Ok(repo.GetByDirector(director));
        }
    }
}