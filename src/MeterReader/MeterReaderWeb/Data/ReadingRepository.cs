using MeterReaderWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderWeb.Data
{
  public class ReadingRepository : IReadingRepository
  {
    private readonly ReadingContext _context;
    private readonly ILogger<ReadingRepository> _logger;

    public ReadingRepository(ReadingContext context, ILogger<ReadingRepository> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
      var result = await _context.Customers
        .Include(c => c.Address)
        .OrderBy(c => c.Name)
        .ToListAsync();

      return result;
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithReadingsAsync()
    {
      var result = await _context.Customers
        .Include(c => c.Address)
        .Include(c => c.Readings)
        .OrderBy(c => c.Name)
        .ToListAsync();

      return result;
    }

    public async Task<Customer> GetCustomerAsync(int id)
    {
      var result = await _context.Customers
        .Include(c => c.Address)
        .OrderBy(c => c.Name)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();

      return result;
    }

    public async Task<Customer> GetCustomerWithReadingsAsync(int id)
    {
      var result = await _context.Customers
        .Include(c => c.Address)
        .Include(c => c.Readings)
        .OrderBy(c => c.Name)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync();

      return result;
    }

    public void AddEntity<T>(T model)
    {
      _context.Add(model);
    }

    public void DeleteEntity<T>(T model)
    {
      _context.Remove(model);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}
