using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebAppPortefolio.Data;

namespace WebAppPortefolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<PortefolioContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }

            }

            host.Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseKestrel(options =>
                          {
                              if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                              {
                                  options.ListenUnixSocket("/tmp/kestrel-assist.sock");    // bind to socket
                              }
                              else
                              {
                                  options.Listen(System.Net.IPAddress.Parse("0.0.0.0"), 6565);
                              }

                          })
                          .UseStartup<Startup>();
        }

    }
}
