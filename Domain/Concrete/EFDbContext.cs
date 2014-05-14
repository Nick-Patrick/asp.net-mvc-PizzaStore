using PizzaShop1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
       // public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Orderline> Orderlines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<PizzaToppingOrder> PizzaToppingOrders { get; set; }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().
              HasMany(c => c.PizzaToppings).              
              WithOptional().
              Map(
               m =>
               {
                   m.MapLeftKey("PizzaId");
                   m.MapRightKey("ToppingId");
                   m.ToTable("PizzaToppings");
               });
        }
        */
    }
}
