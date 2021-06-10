using AutoMapper;
using Chat.Data.Context;
using Chat.Data.GenericRepository.Interface;
using Chat.Data.Models;
using Chat.Data.Repositories;
using Chat.Services;
using Chat.Services.Interfaces;
using ChatAPI.Hubs;
using ChatAPI.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChatAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional:false, reloadOnChange:true).AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDataBase(services);
            ConfigureRepositories(services);
            ConfigureService(services);
            services.AddControllers();
            services.AddSignalR();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddSingleton<IMapper>(sp => AutoMapperConfig.Create());
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChatDbContext>();
                context.Database.Migrate();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }

        private void ConfigureDataBase(IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(opt => opt.UseSqlite("Data Source=Chat.db"));   
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IApplicationRepository<ApplicationUser>, ApplicationUserRepository>();
            services.AddScoped<IApplicationRepository<Message>, MessageRepository>();
            services.AddScoped<IApplicationRepository<Room>, RoomRepository>();

        }
        private void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IApplicationUserService, ApplicationUserService>();

        }
    }
}
