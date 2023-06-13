using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Entities;
using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity;

namespace DataAccessLayer.Context
{
    public class DataContext : DbContext
    {


        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
