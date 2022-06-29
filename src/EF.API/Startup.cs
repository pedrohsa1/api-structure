using System;
using System.Text;
using AutoMapper;
using EF.API.Token;
using EF.API.Token.Config;
using EF.API.ViewModes;
using EF.Domain.Entities;
using EF.Infra.Context;
using EF.Infra.Interfaces;
using EF.Infra.Repositories;
using EF.Services.DTO;
using EF.Services.Interfaces;
using EF.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EF.API
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
            services.AddControllers();

            #region Jwt

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(autenticacao =>
            {
                autenticacao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                autenticacao.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                autenticacao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(autenticacao =>
            {
                autenticacao.RequireHttpsMetadata = false;
                autenticacao.SaveToken = true;
                autenticacao.TokenValidationParameters = tokenValidationParams;
                autenticacao.SaveToken = true;
                autenticacao.Events = new EventosValidacaoTokenJwt();
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("JWT", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .Build());
            });

            #endregion

            #region AutoMapper

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonDTO>().ReverseMap();
                cfg.CreateMap<CreatePersonViewModel, PersonDTO>().ReverseMap();
                cfg.CreateMap<UpdatePersonViewModel, PersonDTO>().ReverseMap();

                cfg.CreateMap<User, UserDTO>().ReverseMap();


            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            #endregion

            #region DI
            services.AddSingleton(d => Configuration);
            services.AddScoped<EFContext>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Projeto API",
                    Version = "v1",
                    Description = @"Neste projeto apresento a resolução do desafio proposto pela GlobalTec. Esta é a estrutura base para o desenvolvimento de microserviços aplicando padrões de projetos. (Usuário: admin / Senha: admin)",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Henrique Souza Arcanjo",
                        Email = "pedro.si.dev@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/pedrohsa1/")
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor utilize Bearer para obter o token de autenticação <TOKEN>",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });

            #endregion

            #region NewtonsoftJson
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EF.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cors =>
                cors.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}