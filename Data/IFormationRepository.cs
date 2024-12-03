using System.Collections.Generic;

namespace Data
{
    public interface IFormationRepository
    {
        List<Formation> GetFormations(int nombre);
        List<Formation> GetAllFormations();
        Formation GetFormationById(int iIdFormation);
        Formation GetFormationByNomSeo(string nomSeo);
    }
}