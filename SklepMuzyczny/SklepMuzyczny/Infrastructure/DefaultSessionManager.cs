using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SklepMuzyczny.Infrastructure
{
    public class DefaultSessionManager : ISessionManager
    {
        private HttpSessionState session;

        public DefaultSessionManager()
        {
            session = HttpContext.Current.Session; 
        }

        public DefaultSessionManager(HttpContext context) // na potrzeby testow
        {
            this.session = context.Session; 
        }

        public void Abandon()
        {
            session.Abandon();
        }

        public T Get<T>(string key)
        {
            return (T)session[key];
        }

        public void Remove<T>(string key)
        {
            session.Remove(key);
        }

        public void Set<T>(string key, T target)
        {
            session.Add(key, target);
        }

        public T TryGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch(NullReferenceException)
            {
                return default(T);
            }
        }
    }
}