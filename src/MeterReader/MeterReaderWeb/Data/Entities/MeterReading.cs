using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderWeb.Data.Entities
{
  public class MeterReading
  {
    public int Id { get; set; }
    public int CustomerId{ get; set; }
    public double Value { get; set; }
    public DateTime ReadingDate { get; set; }
  }
}
