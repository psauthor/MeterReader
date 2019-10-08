using MeterReaderWeb.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderWeb.Data
{
  public class ReadingContext : IdentityDbContext<IdentityUser>
  {
    public ReadingContext(DbContextOptions options) 
      : base(options)
    {}

    public DbSet<Customer> Customers { get; set; }
    public DbSet<MeterReading> Readings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      
      builder.Entity<Customer>()
        .HasData(new Customer()
        {
          Id = 1,
          Name = "Shawn Wildermuth",
          PhoneNumber = "555-1212",
          AddressId = 1
        },
        new Customer()
        {
          Id = 2,
          Name = "Jake Smith",
          PhoneNumber = "(404) 555-1212",
          AddressId = 2
        });

      builder.Entity<Address>()
        .HasData(new Address()
        {
          Id = 1,
          Address1 = "123 Main Street",
          CityTown = "Atlanta",
          StateProvince = "GA",
          PostalCode = "30303"
        }, new Address()
        {
          Id = 2,
          Address1 = "123 Side Street",
          CityTown = "Atlanta",
          StateProvince = "GA",
          PostalCode = "30304"
        });

      builder.Entity<MeterReading>()
        .HasData(new MeterReading()
        {
          Id = 1,
          CustomerId = 1,
          ReadingDate = DateTime.Now,
          Value = 1458.9
        }, new MeterReading()
        {
          Id = 2,
          CustomerId = 1,
          ReadingDate = DateTime.Now,
          Value = 18403.5
        }, new MeterReading()
        {
          Id = 3,
          CustomerId = 2,
          ReadingDate = DateTime.Now,
          Value = 0
        }, new MeterReading()
        {
          Id = 4,
          CustomerId = 2,
          ReadingDate = DateTime.Now,
          Value = 304.75
        }
        );
    }

  }
}
