using Haceb.Demanda.Application.Mappings;
using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Application.UseCase;
using Haceb.Demanda.Domain.IRepository;
using Haceb.Demanda.Domain.Unit;
using Haceb.Demanda.Infrastructure;
using Haceb.Demanda.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using static Haceb.Demanda.Infrastructure.HacebDbContext;

namespace Haceb.Demanda.Api.DI
{
    /// <summary>
    /// Clase encargada de realizar las IoC del proyecto
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Metodo encargado de realizar las Injection de las clases
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            AddRegisterDBContext(services, configuration);
            AddRegisterApplication(services);
            AddRegisterInfrastructure(services);
            AddAuthenticationLib(services, configuration);
            services.AddEndpointsApiExplorer();
            AddSwaggerConf(services);
            Cors(services);
            return services;
        }

        private static void AddRegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HacebDbContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("cnxHaceb")));
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<HacebDbContext>());
        }

        private static void AddRegisterApplication(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<GeneralMapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDemandPort, DemandUseCase>();
            services.AddScoped<IUserPort, UserUseCase>();
            services.AddScoped<ILookupItemPort, LookupItemUseCase>();
        }

        private static void AddRegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDemandRepository, DemandRepository>();
            services.AddScoped<IDemandHistoryRepository, DemandHistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IDemandTypeRepository, DemandTypeRepository>();
        }

        private static void Cors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "politica", builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        private static void AddSwaggerConf(this IServiceCollection services)
        {

            // Agregamos servicios para Swagger
            services.AddSwaggerGen(options =>
            {
                // Configuramos la seguridad del token JWT en Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT con el prefijo 'Bearer '",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Haceb Demanda", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        private static void AddAuthenticationLib(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt");
            var issuer = jwtConfig.GetValue<string>("Issuer");
            var audience = jwtConfig.GetValue<string>("Audience");
            var keyEncript = jwtConfig.GetValue<string>("SecretKey");

            // Configuramos la autenticación JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyEncript))
                };
            });

            services.AddAuthorizationBuilder()
                .AddPolicy("ADM", policy => policy.RequireRole("ADM"))
                .AddPolicy("USR", policy => policy.RequireRole("USR"))
                .AddPolicy("ALL", policy => policy.RequireRole("ADM", "USR"));
        }
    }
}
