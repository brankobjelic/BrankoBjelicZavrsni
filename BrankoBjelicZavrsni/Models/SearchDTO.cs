using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Models
{
    public class SearchDTO
    {
        [Range(10000, 300000)]
        public int Najmanje { get; set; }
        [Range(10000, 300000)]
        public int Najvise { get; set; }
    }
}
