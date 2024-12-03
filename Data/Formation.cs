using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Formation
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string NomSeo { get; set; }
        public string Description { get; set; }
        public List<Avis> Avis { get; set; }
    }
}
