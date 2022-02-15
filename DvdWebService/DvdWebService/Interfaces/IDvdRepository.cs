using DvdWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdWebService
{
    public interface IDvdRepository
    {
        IEnumerable<Dvd> GetAll();
        IEnumerable<Dvd> GetByTitle(string title);
        IEnumerable<Dvd> GetByReleaseYear(string releaseYear);
        IEnumerable<Dvd> GetByDirector(string director);
        IEnumerable<Dvd> GetByRating(string rating);
        Dvd Get(int dvdId);
        void Create(Dvd newDVD);
        void Update(Dvd updatedDVD);
        void Delete(int dvdId);
    }
}
