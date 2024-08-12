using B2B_Project.Domain.Common;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Context
{
    public class B2B_ProjectDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=B2B_ProjectDB;Integrated Security=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Company>()
            //    .HasOne(x => x.PrimaryAppUser)
            //    .WithMany()
            //    .HasForeignKey(x => x.PrimaryAppUserID);

            //builder.Entity<Company>()
            //    .HasOne(x => x.SecondaryAppUser)
            //    .WithMany()
            //    .HasForeignKey(x => x.SecondaryAppUserID);

            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();


            foreach (var item in entries)
            {

                switch (item.State)
                {
                    
                    case EntityState.Deleted:
                        item.Entity.DeletedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid();
                        item.Entity.CreatedDate = DateTime.Now;
                        break;
                    default:
                        break;
                        //case EntityState.Detached:
                        //    break;
                        //case EntityState.Unchanged:
                        //    break;
                }

            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
