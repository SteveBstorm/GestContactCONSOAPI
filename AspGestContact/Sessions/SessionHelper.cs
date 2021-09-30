using AspGestContact.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspGestContact.Sessions
{
    public static class SessionHelper
    {
        public static UserAsp User { get; private set; }

        public static void SetUser(this ISession session, UserAsp Value)
        {
            session.SetString("User", JsonConvert.SerializeObject(Value));
            User = Value;
        }

        public static UserAsp GetUser(this ISession session)
        {
            return JsonConvert.DeserializeObject<UserAsp>(session.GetString("User"));
        }

        public static void Disconnect(this ISession session)
        {
            session.Clear();
            User = null;
        }
    }
}
