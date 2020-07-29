using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Audit.Models;

namespace Audit.DAL
{
    public class ContactContext:DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<LoadFiles> LoadFiles { get; set; }
    }
}