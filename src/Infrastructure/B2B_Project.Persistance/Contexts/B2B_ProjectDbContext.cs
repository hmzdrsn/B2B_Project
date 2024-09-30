using B2B_Project.Domain.Common;
using B2B_Project.Domain.Entities;
using B2B_Project.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B2B_Project.Persistance.Context
{
    public class B2B_ProjectDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=B2B_ProjectDB;Integrated Security=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Order>()
            //       .HasOne(x => x.OrderStatus)
            //       .WithMany() // OrderStatus diğer tarafı tutmayacak
            //       .HasForeignKey(x => x.OrderStatusId);
            //builder.Entity<OrderStatus>()
            //    .HasOne(x => x.Order)
            //    .WithMany()
            //    .HasForeignKey(x => x.OrderId);

            builder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .HasOne(o => o.AppUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Message>()
                .HasOne(x => x.Receiver)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

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
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
