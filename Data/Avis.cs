using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Avis
    {
        public int Id { get; set; }

        [MaxLength(2000)]
        public string Commentaire { get; set; }

        [Required]
        public double Note { get; set; }

        [Required]
        public string NomUtilisateur { get; set; }

        public int FormationId { get; set; }

        public string UserId { get; set; }
        public Formation Formation { get; set; }
       
    }
}
