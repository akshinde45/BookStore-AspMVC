using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FantasticBookStore.Models
{
    public class FantasticContext:DbContext
    {
        public FantasticContext() : base("FantasticDatabase") { }

      public  DbSet<Book> books { get; set; }

      public  DbSet<category> categories { get; set; }

        public DbSet<user> users { get; set; }

        public DbSet<cart> carts { get; set; }

        public DbSet<checkout> checkouts { get; set; }
    }
}