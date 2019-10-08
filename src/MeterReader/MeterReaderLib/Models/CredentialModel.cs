using System.ComponentModel.DataAnnotations;

namespace MeterReaderLib.Models
{
  public class CredentialModel
  {
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Passcode { get; set; }
  }
}
