using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Data
{
    public class FormationMemoryRepository : IFormationRepository
    {
        private List<Formation> _formations = new List<Formation>();

        public FormationMemoryRepository()
        {
            _formations.Add(new Formation { Id = 1, Nom = "Créer un site web avec ASP.Net Core", NomSeo = "asp-net-core", Description = "Grace à cette formation vous saurez créer votre site web en peu de temps" });
            _formations.Add(new Formation { Id = 2, Nom = "Créer un site web avec PHP", NomSeo = "php", Description = "Grace à cette formation vous saurez créer votre site web en peu de temps" });
            _formations.Add(new Formation { Id = 3, Nom = "Devenez un pro du jardinage", NomSeo = "apro-jardinage", Description = "Apprenez à faire pousser des tomates, navets, courgettes et autre fruits et légumes savoureux toute l'année" });
            _formations.Add(new Formation { Id = 4, Nom = "Photo Pro", NomSeo = "photo-pro", Description = "un pro de la photo! vous saurez meme faire des selfies!" });

        }


        public List<Formation> GetFormations(int nombre)
        {
            return _formations.OrderBy(qu => Guid.NewGuid()).Take(nombre).ToList();
        }

        public List<Formation> GetAllFormations()
        {
            return _formations;
        }

        public Formation GetFormationById(int iIdFormation)
        {
            //foreach(var f in _formations)
            // {
            //     if (f.Id == iIdFormation)
            //         return f;
            // }
            // return null;
            
            //return _formations.Where(f => f.Id == iIdFormation).FirstOrDefault();
            return _formations.FirstOrDefault(f => f.Id == iIdFormation);
           
        }
        public Formation GetFormationByNomSeo(string nomSeo)
        {
            return null;
        }

    }
}
