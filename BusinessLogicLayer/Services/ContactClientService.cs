using BusinessLogicLayer.Data;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ContactClientService : IContactClientRepo
    {
        private readonly IContactGlobalRepo _globalService;

        public ContactClientService(IContactGlobalRepo globalService)
        {
            _globalService = globalService;
        }

        public IEnumerable<ContactClient> Get(int userId, string token)
        {
            return _globalService.Get(userId, token).Select(c => c.GlobalToClient());
        }

        public ContactClient Get(int userId, int id)
        {
            return _globalService.GetById(id)?.GlobalToClient();
        }

        public void Insert(ContactClient contact, string token)
        {
            _globalService.Insert(contact.ClientToGlobal(), token);
        }

        public void Update(int id, ContactClient contact, string token)
        {
            _globalService.Update(id, contact.ClientToGlobal(), token);
        }
        public void Delete(int userId, int id, string token)
        {
            _globalService.Delete(id, token);
        }
    }
}
