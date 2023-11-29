global using StoreProgram_.Data;
global using Microsoft.EntityFrameworkCore;
using StoreProgram_.Repository.Interfaces;
using StoreProgram_.Repository;
using StoreProgram_.Service.Interfaces;
using StoreProgram_.Service;
using StoreProgram_;

public class Program 
{
    public static void Main(String[] args) =>
        CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
}


