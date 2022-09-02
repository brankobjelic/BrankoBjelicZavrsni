using BrankoBjelicZavrsni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Interfaces
{
    public interface IAgencyRepository
    {
        IQueryable<Agency> GetAll();
        Agency GetById(int id);
        void Add(Agency agency);
        void Update(Agency agency);
        void Delete(Agency agency);

        IQueryable<AgencyPricesDTO> GetAllWithBiggerTotalPrices(int vrednost);
        IQueryable<AgencyAdvertsDTO> GetAllWithNumOfAdverts();
        IQueryable<Agency> GetAllByName(string name);

    }
}
