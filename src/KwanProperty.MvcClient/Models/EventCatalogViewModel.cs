using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwanProperty.MvcClient.Models
{
    public class EventCatalogViewModel
    {
        public EventCatalogViewModel(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
