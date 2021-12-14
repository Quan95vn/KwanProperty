using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Services
{
    public interface IEventCatalogService
    {
        Task<string> GetAll();
    }
}
