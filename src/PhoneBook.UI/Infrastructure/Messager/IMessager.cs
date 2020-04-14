using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure.Messager
{
    public interface IMessager
    {
        Task<R> Post<T, R>(string url, T payload, params string[] parameters);
        Task<R> Get<R>(string url);
    }
}
