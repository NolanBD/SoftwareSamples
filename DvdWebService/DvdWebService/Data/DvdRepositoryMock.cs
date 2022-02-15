using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DvdWebService.Models
{
    public class DvdRepositoryMock : IDvdRepository
    {
        private static List<Dvd> _dvds;

        static DvdRepositoryMock()
        {
            //populating our mock database with dvd objects
            _dvds = new List<Dvd>()
            {
                new Dvd { id=0, Title="A Great Tale", ReleaseYear="2000", Director="Sam Jones", Rating="G", Notes="This is a great tale!" },
                new Dvd { id=1, Title="A Good Tale", ReleaseYear="2001", Director="Joe Smith", Rating="PG", Notes="This is a good tale!" },
                new Dvd { id=2, Title="A Super Tale", ReleaseYear="2002", Director="John Smith", Rating="PG-13", Notes="This is a super tale!" },
                new Dvd { id=3, Title="A Nice Tale", ReleaseYear="2003", Director="Jane Doe", Rating="R", Notes="This is a nice tale!" },
                new Dvd { id=4, Title="A Bad Tale", ReleaseYear="2004", Director="Grug", Rating="NC-17", Notes="This is a bad tale!" },
            };
        }

        public IEnumerable<Dvd> GetAll()
        {
            return _dvds;
        }

        public Dvd Get(int dvdId)
        {
            return _dvds.FirstOrDefault(d => d.id == dvdId);
        }

        public void Create(Dvd newDVD)
        {
            if (_dvds.Any())
            {
                //if the repository has any dvds in it the new dvd is the highest id + 1
                newDVD.id = _dvds.Max(d => d.id) + 1;
            }
            else
            {
                newDVD.id = 0;
            }

            _dvds.Add(newDVD);
        }

        public void Update(Dvd updatedDVD)
        {
            _dvds.RemoveAll(d => d.id == updatedDVD.id);
            _dvds.Add(updatedDVD);
        }

        public void Delete(int dvdId)
        {
            _dvds.RemoveAll(d => d.id == dvdId);
        }

        public IEnumerable<Dvd> GetByTitle(string title)
        {
            List<Dvd> searchedDvds = new List<Dvd>();

            foreach (var dvd in _dvds)
            {
                if (dvd.Title == title)
                {
                    searchedDvds.Add(dvd);
                }
            }

            return searchedDvds;
        }

        public IEnumerable<Dvd> GetByReleaseYear(string releaseYear)
        {
            List<Dvd> searchedDvds = new List<Dvd>();

            foreach (var dvd in _dvds)
            {
                if (dvd.ReleaseYear.ToString() == releaseYear)
                {
                    searchedDvds.Add(dvd);
                }
            }

            return searchedDvds;
        }

        public IEnumerable<Dvd> GetByDirector(string director)
        {
            List<Dvd> searchedDvds = new List<Dvd>();

            foreach (var dvd in _dvds)
            {
                if (dvd.Director == director)
                {
                    searchedDvds.Add(dvd);
                }
            }

            return searchedDvds;
        }

        public IEnumerable<Dvd> GetByRating(string rating)
        {
            List<Dvd> searchedDvds = new List<Dvd>();

            foreach (var dvd in _dvds)
            {
                if (dvd.Rating == rating)
                {
                    searchedDvds.Add(dvd);
                }
            }

            return searchedDvds;
        }
    }
}