using MeterReader.Data.Entities;

namespace MeterReader.Data;

public interface IReadingRepository
{
  Task<IEnumerable<Customer>> GetCustomersAsync();
  Task<IEnumerable<Customer>> GetCustomersWithReadingsAsync();
  Task<Customer?> GetCustomerAsync(int id);
  Task<Customer?> GetCustomerWithReadingsAsync(int id);

  void AddEntity<T>(T model) where T : notnull;
  void DeleteEntity<T>(T model) where T : notnull;
  Task<bool> SaveAllAsync();
}
