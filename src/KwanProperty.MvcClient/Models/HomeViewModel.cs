using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Models
{
    public class HomeViewModel
    {
        public string Address { get; private set; } = string.Empty;

        public HomeViewModel(string address)
        {
            Address = address;
        }
    }
}
