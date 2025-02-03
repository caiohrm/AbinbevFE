using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CrossCutting.Models;
using System.Reflection;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Configuration;

namespace Infrastructure
{
    public class DefaultContext : DbContext
    {
        public DbSet<Employer> Employers { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        private IConfiguration _configuration { get; set; }
        public DefaultContext(DbContextOptions<DefaultContext> options,IConfiguration configuration) : base(options)
        {
            Database.EnsureCreated();
            _configuration = configuration;
            if (!this.Employers.Any())
            {
                this.Employers.Add(new Employer()
                {
                    FirstName = "Test",
                    LastName = "Application",
                    BirthDate = DateTime.Now.AddYears(-20).ToUniversalTime(),
                    DocNumber = "12345678",
                    Role = CrossCutting.Enums.Role.Director,
                    Email = "test@gmail.com",
                    Password = "$2a$11$cOldWBtpoV7kjOT4V9Ean.N10rmzqSS5Kb44RM6x85ZcQPq7wjP6i",
                    Enabled= true,
                });
                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(connectionString: _configuration.GetConnectionString("DefaultConnection"));
             
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

