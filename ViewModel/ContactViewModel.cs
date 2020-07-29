using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Audit.Models;


namespace Audit.ViewModel
{
    public class ContactViewModel
    {
        
        public Contact Contacts { get; set; }
        public List<LoadFiles> LoadFiles { get; set; }
       
        public SelectList Year { get; set; }
 
    }
}