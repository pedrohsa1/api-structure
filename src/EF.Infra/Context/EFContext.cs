using EF.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EF.Infra.Context
{
    public class EFContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        public EFContext() => this.Database.EnsureCreated();

        public EFContext(DbContextOptions<EFContext> options) : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase("db_memory");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasData(new User(){ 
                Id = 1,
                Username = "admin",
                Password = "admin"
            });
        }
    }
}