using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class CinemaAppDbContext:DbContext
    {
        public CinemaAppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Movie
            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);
            modelBuilder.Entity<Movie>()
                .Property(x => x.Year)
                .IsRequired();
            modelBuilder.Entity<Movie>()
                .Property(x => x.Genre)
                .IsRequired();

            modelBuilder.Entity<Movie>()
                .HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId);

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.MovieReservations)
                .WithOne(x => x.Movie)
                .HasForeignKey(x=>x.MovieId);

            // Director
            modelBuilder.Entity<Director>()
                .Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Director>()
               .Property(x => x.LastName)
               .HasMaxLength(100)
               .IsRequired();
            modelBuilder.Entity<Director>()
                .Property(x => x.Country)
                .HasMaxLength(100)
                .IsRequired();

            // User

            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(100); 
            modelBuilder.Entity<User>()
                 .Property(x => x.UserName)
                 .HasMaxLength(50)
                 .IsRequired(); 
            modelBuilder.Entity<User>()
                 .Property(x => x.Password)
                 .HasMaxLength(100)
                 .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.MovieReservations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<Director>()
                .HasData(new Director()
                {
                    Id=1,
                    Country="Macedonia",
                    FirstName="Name",
                    LastName="Surname"
                },
                new Director()
                {
                    Id = 2,
                    Country = "USA",
                    FirstName = "Name2",
                    LastName = "Surname2"
                }
                );
        }
    }
}
