using BusinessLogicLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IContactClientRepo
    {
        IEnumerable<ContactClient> Get(int userId, string token);
        ContactClient Get(int userId, int id);
        void Insert(ContactClient contact, string token);
        void Update(int id, ContactClient contact, string token);
        void Delete(int userId, int id, string token);
    }
}
