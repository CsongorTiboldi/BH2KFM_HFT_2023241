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
                .HasOne(course => course.Subject)
                .WithMany(subject => subject.Courses)
                .HasForeignKey(course => course.CourseSubject)
                .OnDelete(DeleteBehavior.Cascade)
            );

            modelBuilder.Entity<Course>(course => course
                .HasOne(course => course.Room)
                .WithMany(room => room.Courses)
                .HasForeignKey(course => course.Location)
                .OnDelete(DeleteBehavior.Cascade)
            );

            //Seed data
            modelBuilder.Entity<Subject>().HasData(new Subject[]
            {
                new Subject("1;Introduction to Biology;3;1"),
                new Subject("2;Calculus I;6;1"),
                new Subject("3;General Chemistry;4;1"),
                new Subject("4;Physics for Engineers;3;1"),
                new Subject("5;Introduction to Computer Science;3;2"),
                new Subject("6;Genetics and Genomics;3;2"),
                new Subject("7;Organic Chemistry;4;2"),
                new Subject("8;Data Structures and Algorithms;6;2"),
                new Subject("9;Environmental Science;3;2"),
                new Subject("10;Differential Equations;3;3"),
                new Subject("11;Introduction to Robotics;3;3"),
                new Subject("12;Materials Science;4;3"),
                new Subject("13;Microbiology;3;3"),
                new Subject("14;Digital Electronics;3;3"),
                new Subject("15;Statistics and Probability;3;4"),
                new Subject("16;Quantum Mechanics;3;4"),
                new Subject("17;Engineering Mechanics;3;4"),
                new Subject("18;Machine Learning;4;4"),
                new Subject("19;Cell Biology;5;4"),
                new Subject("20;Thermodynamics;3;5"),
                new Subject("21;Bioinformatics;3;5"),
                new Subject("22;Computer Networks;3;5"),
                new Subject("23;Human Anatomy and Physiology;4;6"),
                new Subject("24;Artificial Intelligence;6;6"),
                new Subject("25;Geology and Earth Science;3;6")
            });

            modelBuilder.Entity<Room>().HasData(new Room[]
            {
                new Room("1;100;true"),
                new Room("2;45;false"),
                new Room("3;250;true"),
                new Room("4;70;false"),
                new Room("5;350;true"),
                new Room("6;30;false"),
                new Room("7;150;true"),
                new Room("8;200;false"),
                new Room("9;80;true"),
                new Room("10;400;false")
            });

            modelBuilder.Entity<Course>().HasData(new Course[]
            {
                new Course("1;2023/09/04 08:00;2023/09/04 09:30;7;3"),
                new Course("2;2023/09/05 10:00;2023/09/05 11:30;12;8"),
                new Course("3;2023/09/06 14:30;2023/09/06 16:00;4;1"),
                new Course("4;2023/09/07 09:00;2023/09/07 10:30;19;5"),
                new Course("5;2023/09/08 13:30;2023/09/08 15:00;8;9"),
                new Course("6;2023/09/04 11:00;2023/09/04 12:30;22;2"),
                new Course("7;2023/09/05 14:30;2023/09/05 16:00;11;6"),
                new Course("8;2023/09/06 09:00;2023/09/06 10:30;16;4"),
                new Course("9;2023/09/07 13:30;2023/09/07 15:00;3;7"),
                new Course("10;2023/09/08 10:00;2023/09/08 11:30;14;10"),
                new Course("11;2023/09/04 16:00;2023/09/04 17:30;6;1"),
                new Course("12;2023/09/05 08:30;2023/09/05 10:00;25;5"),
                new Course("13;2023/09/06 11:30;2023/09/06 13:00;9;8"),
                new Course("14;2023/09/07 14:00;2023/09/07 15:30;18;2"),
                new Course("15;2023/09/08 09:30;2023/09/08 11:00;21;10"),
                new Course("16;2023/09/04 10:30;2023/09/04 12:00;10;7"),
                new Course("17;2023/09/05 16:00;2023/09/05 17:30;15;3"),
                new Course("18;2023/09/06 08:00;2023/09/06 09:30;24;6"),
                new Course("19;2023/09/07 11:00;2023/09/07 12:30;2;9"),
                new Course("20;2023/09/08 14:30;2023/09/08 16:00;13;4"),
                new Course("21;2023/09/04 13:00;2023/09/04 14:30;5;10"),
                new Course("22;2023/09/05 09:30;2023/09/05 11:00;20;8"),
                new Course("23;2023/09/06 16:00;2023/09/06 17:30;12;3"),
                new Course("24;2023/09/07 08:30;2023/09/07 10:00;7;5"),
                new Course("25;2023/09/08 11:30;2023/09/08 13:00;22;1"),
                new Course("26;2023/09/04 15:00;2023/09/04 16:30;3;2"),
                new Course("27;2023/09/05 12:00;2023/09/05 13:30;19;9"),
                new Course("28;2023/09/06 13:30;2023/09/06 15:00;4;6"),
                new Course("29;2023/09/07 16:00;2023/09/07 17:30;11;10"),
                new Course("30;2023/09/08 08:00;2023/09/08 09:30;16;7"),
                new Course("31;2023/09/04 09:30;2023/09/04 11:00;14;3"),
                new Course("32;2023/09/05 13:00;2023/09/05 14:30;21;8"),
                new Course("33;2023/09/06 14:00;2023/09/06 15:30;8;2"),
                new Course("34;2023/09/07 10:30;2023/09/07 12:00;25;5"),
                new Course("35;2023/09/08 16:00;2023/09/08 17:30;6;4"),
                new Course("36;2023/09/04 11:30;2023/09/04 13:00;17;10"),
                new Course("37;2023/09/05 10:00;2023/09/05 11:30;9;1"),
                new Course("38;2023/09/06 09:30;2023/09/06 11:00;23;7"),
                new Course("39;2023/09/07 14:30;2023/09/07 16:00;1;6"),
                new Course("40;2023/09/08 13:00;2023/09/08 14:30;15;8")
            });
        }
    }
}
