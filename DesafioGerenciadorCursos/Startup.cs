using DesafioGerenciadorCursos.Data;
using DesafioGerenciadorCursos.Repository;
using DesafioGerenciadorCursos.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var configuracoesSection = Configuration.GetSection("ConfiguracoesJWT");
            var configuracoesJWT = configuracoesSection.Get<ConfiguracoesJWT>();

            services.Configure<ConfiguracoesJWT>(configuracoesSection); 

            services.AddAuthentication(opcoes =>
            {
                opcoes.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opcoes.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(opcoes =>
                {
                    opcoes.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuracoesJWT.Segredo)),
                        ValidAudience = "https://localhost:5001",
                        ValidIssuer = "Bootcamp2022"
                    };
                });


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MeuSqlServer"));
            });


            services.AddScoped<IRepository, CursoRepository>();

            services.AddControllers();

            services.AddSwaggerExtension();
                     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //vou deixar Swagger disponível apenas no ambiente de desenvolvimento
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioGerenciadorCursos v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
