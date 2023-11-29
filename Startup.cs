using StoreProgram_.Repository.Interfaces;
using StoreProgram_.Repository;
using StoreProgram_.Service.Interfaces;
using StoreProgram_.Service;


namespace StoreProgram_
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration
                    .GetConnectionString("DefaultConnection"));
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379, ssl=false, abortConnect=false, password=45vehupe";
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<IUnityOfWorkRepository, UnityOfWorkRepository>();

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Program).Assembly);

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
