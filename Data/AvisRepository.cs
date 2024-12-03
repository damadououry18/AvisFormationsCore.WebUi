using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data
{
    public class AvisRepository : IAvisRepository
    {
        MonDbContext _context;
        public AvisRepository(MonDbContext context)
        {
            _context = context;
        }

        public List<Avis> GetAllAvis()
        {
            return _context.Avis.ToList();
        }
        public Avis GetAvisById(int iIdAvis)
        {
            return _context.Avis.FirstOrDefault(f => f.Id == iIdAvis);
        }
        //public async Task<long> UpdateStudent(Student student)
        //{
        //    long updatedStudentId = 0;
        //    var studentInDb = await GetStudentById(student.Id, student.SchoolId);
        //    if (studentInDb != null)
        //    {
        //        _context.Students.Update(student);
        //        updatedStudentId = await _context.SaveChangesAsync();
        //    }
        //    return updatedStudentId;
        //}
        public void UpdateAvis(Avis avis)
        {

            var existingAvis = _context.Avis.Find(avis.Id); // Charger l'entité existante
            if (existingAvis != null)
            {
                // Mettre à jour les valeurs de l'entité suivie
                _context.Entry(existingAvis).CurrentValues.SetValues(avis);
            }
            else
            {
                throw new KeyNotFoundException($"L'avis avec l'ID {avis.Id} n'existe pas.");
            }

            _context.SaveChanges();

            // debut original
            //_context.Entry(avis).State = EntityState.Modified;
            //_context.SaveChanges();

            // fin original

            //long updatedAvisId = 0;
            //var avisInDb = await GetAvisById(avis.Id);
            //if (avisInDb != null)
            //{
            //    _context.Avis.Update(avis);
            //    //updatedAvisId = await _context.SaveChangesAsync();
            //    _context.SaveChanges();
            //}
            //return updatedAvisId;



            //_context.Update(avis);

            //_context.Entry(avis).State = EntityState.Detached;
            //_context.SaveChanges();
        }
       
        public void DeleteAvisById(int iIdAvis)
        {

            _context.Avis.Remove(_context.Avis.Find(iIdAvis));
            _context.SaveChanges();
            
        }
        public void SaveAvis(string commentaire, string nom, string idFormation,string note, string userId)
        {
            int iIdFormation = -1;
            if (!Int32.TryParse(idFormation, out iIdFormation))
            {
                return;
            }

            double dNote = -1;
            if (!Double.TryParse(note, NumberStyles.Any, CultureInfo.InvariantCulture, out dNote))
            {
                return;
            }

            var avis = new Avis();
            avis.Commentaire = commentaire;
            avis.NomUtilisateur = nom;
            avis.FormationId = iIdFormation;
            avis.Note = dNote;
            avis.UserId = userId;

            _context.Avis.Add(avis);
            _context.SaveChanges();
        }

        public bool EstAutoriseACommenter(string userId, int idFormation)
        {

            var personEntity = _context.Avis.FirstOrDefault(p => p.UserId == userId && p.FormationId == idFormation);
            if (personEntity != null)
            {
                return true;
            }
            return false;
        }
    }
}
