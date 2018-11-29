using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_AuthorizationBeispiel.Models
{
  public class User
  {
    public string Name { get; set; }
    public string[] Roles { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string RestrictToContinent { get; set; } 

    public bool? HasChildren { get; set; }

    public bool IsExpert { get; set; } = false;
  }

  public class UserRepo
  {
    public IEnumerable<User> Users { get; set; } = new List<User>
    {
      new User{Name="Peter", Roles=new string[]{"Guest" }, DateOfBirth=new DateTime(1950,3,2), HasChildren=true },
      new User{Name="Paul", Roles=new string[]{"Admin","Guest" }, DateOfBirth=new DateTime(1960,6,12), HasChildren=false },
      new User{Name="Mary", Roles=new string[]{"Admin" }, DateOfBirth=new DateTime(1970,7,22), IsExpert=true, RestrictToContinent="europe" }
    };
  }

}
