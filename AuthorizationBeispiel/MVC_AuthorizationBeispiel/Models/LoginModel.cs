using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Models
{
  public class LoginModel
  {
    public string ReturnUrl { get; set; }
    public string Username { get; set; }
  }
}
