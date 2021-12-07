namespace MeterReader.Data.Entities;

public class MeterReading
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  public double Value { get; set; }
  public DateTime ReadingDate { get; set; }
}
