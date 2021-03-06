﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.UI.Infrastructure.Messager
{
    public interface IMessager
    {
        Task<TResult> Post<TPayload, TResult>(string url, TPayload payload, string jwtToken=null);
        Task<TResult> Put<TPayload, TResult>(string url, TPayload payload, string jwtToken=null);
        Task<TResult> Get<TResult>(string url, string jwtToken=null);
    }
}
