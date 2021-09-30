using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IContactGlobalRepo
    {
        IEnumerable<ContactGlobal> Get(int userID, string token);
        ContactGlobal GetById(int contactId);
        void Insert(ContactGlobal contact, string token);
        void Update(int id, ContactGlobal contact, string token);
        void Delete(int contactId, string token);
    }
}
