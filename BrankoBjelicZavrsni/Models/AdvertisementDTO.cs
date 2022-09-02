using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Models
{
    public class AdvertisementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EstateType { get; set; }
        public int YearConstructed { get; set; }
        public int EstatePrice { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AdvertisementDTO dTO &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   EstateType == dTO.EstateType &&
                   YearConstructed == dTO.YearConstructed &&
                   EstatePrice == dTO.EstatePrice &&
                   AgencyId == dTO.AgencyId &&
                   AgencyName == dTO.AgencyName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, EstateType, YearConstructed, EstatePrice, AgencyId, AgencyName);
        }
    }
}
