using System.Collections.Generic;
using System.Threading.Tasks;
using MeterReaderWeb.Data.Entities;

namespace MeterReaderWeb.Data
{
  public interface IReadingRepository
  {
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<IEnumerable<Customer>> GetCustomersWithReadingsAsync();
    Task<Customer> GetCustomerAsync(int id);
    Task<Customer> GetCustomerWithReadingsAsync(int id);

    void AddEntity<T>(T model);
    void DeleteEntity<T>(T model);
    Task<bool> SaveAllAsync();
  }
}