using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi.Models
{
    public class LaisserUnAvisViewModel
    {
        [StringLength(100, ErrorMessage = "Le texte dépasse la taille maximale autorisée")]
        public string Commentaire { get; set; }

        //[Required(ErrorMessage = "Please enter le nom")]
        //public string Nom { get; set; }

        [Required]
        public string Note { get; set; }
        [Required]
        public string IdFormation { get; set; }

        public string NomFormation { get; set; }
    }
}
