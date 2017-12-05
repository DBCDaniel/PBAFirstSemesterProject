using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InterfaceMVC.Models
{
    public class OurDbContext: DbContext
    {
        public DbSet<UserAccount> userAccount { get; set; }

        public DbSet<ClearanceLevel> clearanceLevel { get; set; }
        public DbSet<EventLog> eventLog { get; set; }
    }
}