using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<UserImage> UserImages { get; set; }

 #region FluentApi

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
           .HasOne(c => c.Month)
           .WithMany(u => u.Days)
           .HasForeignKey(c => c.MonthId)
           .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Day>()
          .HasOne(c => c.User)
          .WithMany(u => u.Days)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

           modelBuilder.Entity<Expense>()
          .HasOne(c => c.User)
          .WithMany(u => u.Expenses)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Expense>()
           .HasOne(c => c.Day)
           .WithMany(u => u.Expenses)
           .HasForeignKey(c => c.DayId)
           .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Expense>()
           .HasOne(c => c.Currency)
           .WithMany(u => u.Expenses)
           .HasForeignKey(c => c.CurrencyId)
           .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Expense>()
            .HasOne(c => c.Category)
            .WithMany(u => u.Expenses)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Income>()
            .HasOne(c => c.User)
            .WithMany(u => u.Incomes)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<Income>()
          .HasOne(c => c.Day)
          .WithMany(u => u.Incomes)
          .HasForeignKey(c => c.DayId)
          .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 
            modelBuilder.Entity<Income>()
          .HasOne(c => c.Currency)
          .WithMany(u => u.Incomes)
          .HasForeignKey(c => c.CurrencyId)
          .OnDelete(DeleteBehavior.Cascade);//When Company is deleted, it's owner should be deleted alongside 

            modelBuilder.Entity<User>()
            .HasOne<UserImage>()
            .WithOne(ui => ui.User)
            .HasForeignKey<UserImage>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade); //when the User is gone, User's image is gone with him

            modelBuilder.Entity<UserImage>()
        .HasKey(u => u.Id);
        }
    #endregion
    }   
}