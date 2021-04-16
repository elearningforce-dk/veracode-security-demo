using System.Data.Entity;

namespace VeraDemoNet.DataAccess
{
    public class BlabberDB : DbContext  
    {
        public BlabberDB() : base("connectionString")  
        {  
        }  
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)  
        {  
            modelBuilder.Entity<User>().HasKey(x=>x.UserName);
        }
          
        public DbSet<User> Users { get; set; }  
    }  
}