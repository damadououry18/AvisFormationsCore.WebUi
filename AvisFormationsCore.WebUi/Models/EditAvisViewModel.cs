using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AvisFormationsCore.WebUi.Models
{
    public class EditAvisViewModel
    {
        public int Id { get; set; }
        public string Commentaire { get; set; }
        public double Note { get; set; }
        public string NomUtilisateur { get; set; }
        public string NomSeo { get; set; }
        public int FormationId { get; set; }
        public IEnumerable<SelectListItem> StatusList { get; set; }
    }
    
    
}
