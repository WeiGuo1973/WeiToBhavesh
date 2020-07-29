using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Audit.Models;

namespace Audit.ViewModel
{
    public class DisplayViewModel
    {
        public Contact Contacts { get; set; }
        public string SortBy { get; set; }

        public string Search { get; set; }
    }
}