using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepMuzyczny.Infrastructure
{
    public interface ISessionManager
    {
        T Get<T>(string key);
        T TryGet<T>(string key);
        void Set<T>(string key, T target);
        void Abandon();
        void Remove<T>(string key);
    }
}
