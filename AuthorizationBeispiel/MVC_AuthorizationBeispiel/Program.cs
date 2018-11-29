using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MVC_AuthorizationBeispiel
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
      .ConfigureAppConfiguration((bc, config)=>
      {
        string path = Path.Combine(bc.HostingEnvironment.ContentRootPath, "users.json");
        config.AddJsonFile(path, false, true);
      });
  }
}
