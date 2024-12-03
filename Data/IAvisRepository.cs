using System.Collections.Generic;

namespace Data
{
    public interface IAvisRepository
    {
        List<Avis> GetAllAvis();
        Avis GetAvisById(int iIdAvis);
        void UpdateAvis(Avis avis);
        void SaveAvis(string commentaire, string nom, string idFormation, string note, string userId);
        
        void DeleteAvisById(int pId);
        //void Update
        bool EstAutoriseACommenter(string userId, int idFormation);
    }
}