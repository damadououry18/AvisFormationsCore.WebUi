using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ContactRepository : IContactRepository
    {
        MonDbContext _context;
        public ContactRepository(MonDbContext context)
        {
            _context = context;
        }
        public void SaveMessage(string nom, string email, string message)
        {
            var msg = new ContactMessage();
            msg.Email = email;
            msg.Nom = nom;
            msg.Message = message;

            _context.Messages.Add(msg);
            _context.SaveChanges();
        }
    }

    
    public interface IContactRepository
    {
        void SaveMessage(string nom, string email, string message);
    }
}
