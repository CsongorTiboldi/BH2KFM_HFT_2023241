using Microsoft.EntityFrameworkCore;
using BH2KFM_HFT_2023241.Models;
using System;

namespace BH2KFM_HFT_2023241.Repository
{
    public class LectureDbContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Course> Courses { get; set; }

        public LectureDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("lectures")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Foreign key references
            modelBuilder.Entity<Course>( course => course
                .HasOne<Subject>()
                .WithMany()
                .HasForeignKey(course => course.CourseSubject)
                .OnDelete(DeleteBehavior.Cascade)
            );

            modelBuilder.Entity<Course>(course => course
                .HasOne<Room>()
                .WithMany()
                .HasForeignKey(course => course.CourseSubject)
                .OnDelete(DeleteBehavior.Cascade)
            );

        }
    }
}
