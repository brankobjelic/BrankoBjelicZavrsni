using BrankoBjelicZavrsni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Interfaces
{
    public interface IAdvertisementRepository
    {
        IQueryable<Advertisement> GetAll();
        Advertisement GetById(int id);
        void Add(Advertisement advertisement);
        void Update(Advertisement advertisement);
        void Delete(Advertisement advertisement);

        IQueryable<Advertisement> GetAllByType(string type);
        IQueryable<Advertisement> GetAllBySearchPrice(SearchDTO search);

    }
}
