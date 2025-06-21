using Adapters.Output.Persistence;
using Application.Mapper;
using Application.Services;
using Application.Services.Interfaces;
using Domain.Ports.Output;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddMineSafeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BaseDatosConfig>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped<SqlConnection>(provider =>
            {
                var connectionString = configuration.GetConnectionString("MineSafeCore");
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IDbConnection>(provider =>
                provider.GetRequiredService<SqlConnection>());


            //Repositories
            services.AddScoped<ITipoCondicionRepository, TipoCondicionRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IReporteActaRepository, ReporteActaRepository>();
            services.AddScoped<IMedidaCorrectivaRepository, MedidaCorrectivaRepository>();
            services.AddScoped<IFotoCondicionRepository, FotoCondicionRepository>();


            //Services
            services.AddScoped<ITipoCondicionService, TipoCondicionService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IReporteActaService, ReporteActaService>();
            services.AddScoped<IMedidaCorrectivaService, MedidaCorrectivaService>();
            services.AddScoped<IFotoCondicionService, FotoCondicionService>();


            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
