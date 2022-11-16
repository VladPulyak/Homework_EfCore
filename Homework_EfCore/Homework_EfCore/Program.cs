using System.Reflection;
using Homework_EfCore;
using Homework_EfCore.Extensions;

CreateHostBuilder(args).Build().Run();
static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
}