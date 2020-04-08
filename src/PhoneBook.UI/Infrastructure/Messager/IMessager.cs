using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure.Messager
{
    public interface IMessager
    {
        R Post<T, R>(string url, T payload, params string[] parameters);
        R Get<R>(string url);
    }
}
