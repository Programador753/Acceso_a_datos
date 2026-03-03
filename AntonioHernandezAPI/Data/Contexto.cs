using AntonioHernandezAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AntonioHernandezAPI.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<MealIngredient> MealIngredient { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MealIngredient>().HasKey(mi => new { mi.IdMeal, mi.IdIngredient });

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Meals)
                .HasForeignKey(m => m.IdCategory);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Area)
                .WithMany(a => a.Meals)
                .HasForeignKey(m => m.IdArea);

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Meal)
                .WithMany(m => m.MealIngredients)
                .HasForeignKey(mi => mi.IdMeal);

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Ingredient)
                .WithMany(i => i.MealIngredients)
                .HasForeignKey(mi => mi.IdIngredient);
        }
    }
}