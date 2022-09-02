using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrankoBjelicZavrsni.Models
{
    public class Agency
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        [Range(1980, 2022)]
        public int YearFounded { get; set; }
    }
}
