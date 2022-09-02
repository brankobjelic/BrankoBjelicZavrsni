using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength =2)]
        public string EstateType { get; set; }
        [Required]
        [Range(1910, 2022)]
        public int YearConstructed { get; set; }
        [Required]
        [Range(10000, 300000)]
        public int EstatePrice { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
