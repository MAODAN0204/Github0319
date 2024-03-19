using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wyer.Services
{
    internal interface AlertService
    {
        Task ShowAsync(string message);
    }

    
}
