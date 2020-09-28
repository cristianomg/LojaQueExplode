using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Business.Services.Concrete;
using Br.Com.LojaQueExplode.Domain.Configurations;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.Repositories.Concrete;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Concrete;
using Br.Com.LojaQueExplode.Util.Security.Abstract;
using Br.Com.LojaQueExplode.Util.Security.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace Br.Com.LojaQueExplode.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("LojaQueExplodeContext");
            services.AddDbContext<LojaQueExplodeContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddControllers();

            var secretKey = Configuration.GetValue<string>("SecretKey");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SampleApi",
                        Version = "v1",
                        Description = "Api privada Loja que Explode",
                        Contact = new OpenApiContact
                        {
                            Name = "Cristiano Macedo / Yan Kelvin",
                            Url = new Uri("https://github.com/cristianomg")
                        }
                    });
            });


            AutoMapperConfiguration(services);
            RegisterUtilities(services);
            RegisterServices(services);
            RegisterRepostories(services);
            RegisterUnitOfWork(services);

            services.AddSingleton<BaseConfigurations, BaseConfigurations>(op =>
            {
                var obj = new BaseConfigurations();
                obj.SecretKey = secretKey;

                return obj;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Api V1");
                c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
            });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICreateUserService, CreateUserService>();
            services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
        }

        private void RegisterRepostories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IPermissionRepository, EFPermissionRepository>();
        }
        private void RegisterUnitOfWork(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void RegisterUtilities(IServiceCollection services)
        {
            services.AddSingleton<IEncryption, EncryptionSHA256>();
        }

        private void AutoMapperConfiguration(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, DTOUser>();
                config.CreateMap<ResultAuthentication, DTOResultAuthentication>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
