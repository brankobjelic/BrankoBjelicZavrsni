using BrankoBjelicZavrsni.Interfaces;
using BrankoBjelicZavrsni.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly AppDbContext _context;
        public AdvertisementRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Advertisement> GetAll()
        {
            return _context.Advertisements.Include(f => f.Agency);
        }

        public Advertisement GetById(int id)
        {
            return _context.Advertisements.Include(f => f.Agency).FirstOrDefault(f => f.Id == id);
        }

        public void Add(Advertisement Advertisement)
        {
            _context.Add(Advertisement);
            _context.SaveChanges();
        }

        public void Delete(Advertisement Advertisement)
        {
            _context.Remove(Advertisement);
            _context.SaveChanges();
        }

        public void Update(Advertisement Advertisement)
        {
            _context.Entry(Advertisement).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IQueryable<Advertisement> GetAllByType(string type)
        {
            return _context.Advertisements.Include(f => f.Agency).Where(a => a.EstateType.Contains(type)).OrderBy(a => a.EstatePrice);

        }

        public IQueryable<Advertisement> GetAllBySearchPrice(SearchDTO search)
        {
            return _context.Advertisements.Include(a => a.Agency).Where(a => a.EstatePrice >= search.Najmanje && a.EstatePrice <= search.Najvise).OrderBy(a => a.EstatePrice);
        }
    }
}
