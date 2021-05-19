using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ST_JuniorProject.Services.Implementations;
using ST_JuniorProject.Services.Interfaces;
using ST_JuniorProject.Models;
using ST_JuniorProject.Repositories.Interfaces;
using ST_JuniorProject.Repositories.Implementations;

namespace ST_JuniorProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc();
            services.AddDbContext<AppDBContext>(opt => opt.UseSqlServer(connection));
            services.AddControllers();

            services.AddTransient<IUserInfoUpdateService, UserInfoUpdateService>();
            services.AddTransient<IUserRepository<UserData>, UserRepository<UserData>>();
            services.AddTransient<IUserRepository<UserContact>, UserRepository<UserContact>>();
            services.AddTransient<IUserRepository<CRMUserInfo>, UserRepository<CRMUserInfo>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}