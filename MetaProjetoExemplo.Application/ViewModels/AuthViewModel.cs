
using System.ComponentModel.DataAnnotations;

namespace MetaProjetoExemplo.Application.ViewModels
{
  public class Login
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [MinLength(3)]
    public string Password { get; set; }

  }

}