using BrankoBjelicZavrsni.Interfaces;
using BrankoBjelicZavrsni.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Repository
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly AppDbContext _context;
        public AgencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Agency> GetAll()
        {
            return _context.Agencies;
        }

        public Agency GetById(int id)
        {
            return _context.Agencies.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Agency Agency)
        {
            _context.Add(Agency);
            _context.SaveChanges();
        }

        public void Delete(Agency Agency)
        {
            _context.Remove(Agency);
            _context.SaveChanges();
        }

        public void Update(Agency Agency)
        {
            _context.Entry(Agency).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IQueryable<AgencyPricesDTO> GetAllWithBiggerTotalPrices(int vrednost)
        {
            return _context.Advertisements.Include(a => a.Agency).GroupBy(g => g.AgencyId).Select(r => new AgencyPricesDTO
            {
                Agency = _context.Agencies.Where(a => a.Id == r.Key).Select(a => a.Name).Single(),
                TotalPrices = r.Sum(r => r.EstatePrice)
            }).Where(r => r.TotalPrices > vrednost).OrderBy(r => r.Agency);
        }

        public IQueryable<AgencyAdvertsDTO> GetAllWithNumOfAdverts()
        {
            return _context.Advertisements.Include(a => a.Agency).GroupBy(g => g.AgencyId).Select(r => new AgencyAdvertsDTO
            {
                Agency = _context.Agencies.Where(a => a.Id == r.Key).Select(a => a.Name).Single(),
                AdvertNumber = r.Select(r => r.Id).Count()
            }
            ).OrderByDescending(r => r.AdvertNumber);
        }

        public IQueryable<Agency> GetAllByName(string name)
        {
            return _context.Agencies.Where(a => a.Name == name).OrderBy(a => a.YearFounded).ThenByDescending(a => a.Name);

        }

    }
}
