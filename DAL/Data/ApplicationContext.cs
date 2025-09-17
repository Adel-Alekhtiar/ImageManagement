using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //setting the connectionString in the Program.cs
            //using the AddDAL extension method
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPH inheritance, string values for discriminator can be anything
            // there is kwarky way to upgrade lead to customer usng EFcore
            modelBuilder.Entity<ContactBase>()
                .HasDiscriminator<string>("ContactType")
                .HasValue<Customer>("Customer")
                .HasValue<Lead>("Lead");
            modelBuilder.Entity<Image>()
                .HasOne(i => i.ContactBase)
                .WithMany(c => c.Images)
                .HasForeignKey(i => i.ContactBaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet <ContactBase> ContactBase{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
