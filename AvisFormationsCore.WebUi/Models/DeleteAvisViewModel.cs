using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvisFormationsCore.WebUi.Models
{
    public class DeleteAvisViewModel
    {
        public int Id { get; set; }
        public string Commentaire { get; set; }
        public double Note { get; set; }
        public string NomUtilisateur { get; set; }
        public string NomFormation { get; set; }
    }
}
