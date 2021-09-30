using AspGestContact.Models;
using AspGestContact.Models.Forms;
using BusinessLogicLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspGestContact.Mappers
{
    public static class Mapper
    {
        public static UserClient RegisterToClient(this RegisterForm user)
        {
            return new UserClient(user.LastName, user.FirstName, user.Email, user.Passwd);
        }

        public static UserAsp ClientToAsp(this UserClient user)
        {
            return new UserAsp()
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Token = user.Token
            };
        }
        public static ContactClient AspToClient(this ContactAsp c)
        {
            return new ContactClient(c.LastName, c.FirstName, c.Email, c.Phone, c.BirthDate, c.UserId);
        }

        public static ContactAsp ClientToAsp(this ContactClient c)
        {
            return new ContactAsp
            {
                Id = c.Id,
                LastName = c.LastName,
                FirstName = c.FirstName,
                Email = c.Email,
                Phone = c.Phone,
                BirthDate = c.BirthDate,
                UserId = c.UserId
            };
        }

    }
}
