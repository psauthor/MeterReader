using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MeterReader.Data;

public class ReadingContext : IdentityDbContext<IdentityUser>
{
  public ReadingContext(DbContextOptions options)
    : base(options)
  { }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<MeterReading> Readings => Set<MeterReading>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Customer>()
    .HasData(new 
     {
       Id = 1,
       Name = "Shawn Wildermuth",
       PhoneNumber = "555-1212",
       AddressId = 1
     },
    new 
    {
      Id = 2,
      Name = "Jake Smith",
      PhoneNumber = "(404) 555-1212",
      AddressId = 2
    });

    builder.Entity<Address>()
      .HasData(new
      {
        Id = 1,
        Address1 = "123 Main Street",
        CityTown = "Atlanta",
        StateProvince = "GA",
        PostalCode = "30303"
      }, new 
      {
        Id = 2,
        Address1 = "123 Side Street",
        CityTown = "Atlanta",
        StateProvince = "GA",
        PostalCode = "30304"
      });

    builder.Entity<MeterReading>()
      .HasData(new
       {
         Id = 1,
         CustomerId = 1,
         ReadingDate = DateTime.Now,
         Value = 1458.9
       }, new 
       {
         Id = 2,
         CustomerId = 1,
         ReadingDate = DateTime.Now,
         Value = 18403.5
       }, new 
       {
         Id = 3,
         CustomerId = 2,
         ReadingDate = DateTime.Now,
         Value = 0d
       }, new 
       {
         Id = 4,
         CustomerId = 2,
         ReadingDate = DateTime.Now,
         Value = 304.75
       }
      );
  }

}
