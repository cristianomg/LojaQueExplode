using AutoMapper;
using Br.Com.LojaQueExplode.Api.Models;
using Br.Com.LojaQueExplode.Business.DTOs;
using Br.Com.LojaQueExplode.Business.Services.Abstract;
using Br.Com.LojaQueExplode.Business.Services.Concrete;
using Br.Com.LojaQueExplode.Business.WebServices.Abstract;
using Br.Com.LojaQueExplode.Business.WebServices.Concrete;
using Br.Com.LojaQueExplode.Domain.Configurations;
using Br.Com.LojaQueExplode.Domain.Entities;
using Br.Com.LojaQueExplode.Infra.Context;
using Br.Com.LojaQueExplode.Infra.Repositories.Abstract;
using Br.Com.LojaQueExplode.Infra.Repositories.Concrete;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Abstract;
using Br.Com.LojaQueExplode.Infra.UnitOfWork.Concrete;
using Br.Com.LojaQueExplode.Util.Security.Abstract;
using Br.Com.LojaQueExplode.Util.Security.Concrete;
using Br.Com.LojaQueExplode.Util.Smtp.Abstract;
using Br.Com.LojaQueExplode.Util.Smtp.Concrete;
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
                options.UseNpgsql(connectionString));

            services.AddControllers();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            string JWT_SECRET_KEY = Configuration.GetValue<string>("Jwt.SecretKey");
            string IMGBB_SECRET_KEY = Configuration.GetValue<string>("Jwt.SecretKey");

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_SECRET_KEY)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            AutoMapperConfiguration(services);
            RegisterUtilities(services);
            RegisterServices(services);
            RegisterRepostories(services);
            RegisterUnitOfWork(services);
            RegisterWebServices(services);

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<BearerAuthenticationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
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

            services.AddSingleton<BaseConfigurations, BaseConfigurations>(op =>
            {
                var obj = new BaseConfigurations
                {
                    JwtSecretKey = JWT_SECRET_KEY,
                    ImgbbSecretKey = IMGBB_SECRET_KEY
                };

                return obj;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<LojaQueExplodeContext>();
                context.Database.Migrate();
            }

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
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<ICreateCategoryService, CreateCategoryService>();
            services.AddScoped<ICreateProductService, CreateProductService>();
            services.AddScoped<IAddProductOnShoppingCartService, AddProductOnShoppingCartService>();
            services.AddScoped<IRemoveProductOnShoppingCartService, RemoveProductOnShoppingCartService>();
            services.AddScoped<IGetOrCreateShoppingCartOpenedService, GetOrCreateShoppingCartService>();
            services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
            services.AddScoped<ISendMailService, SendMailService>();

        }

        private void RegisterRepostories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IPermissionRepository, EFPermissionRepository>();
            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IShoppingCartRepository, EFShoppingCartRepository>();
            services.AddTransient<IPurchaseStatusRepository, EFPurchaseStatusRepository>();
        }
        private void RegisterUnitOfWork(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private void RegisterWebServices(IServiceCollection services)
        {
            services.AddSingleton<IImageStorageWebService, ImgBBStorageWebService>();
        }

        private void RegisterUtilities(IServiceCollection services)
        {
            services.AddSingleton<IEncryption, EncryptionSHA256>();
        }

        private void AutoMapperConfiguration(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<User, DTOUser>().ReverseMap();
                 
                config.CreateMap<AuthenticationResult, DTOResultAuthentication>().ReverseMap();
                config.CreateMap<Product, DTOProduct>().ReverseMap();
                config.CreateMap<Category,DTOCategory>().ReverseMap();
                config.CreateMap<ComplementaryProductData, DTOComplementaryProductData>().ReverseMap();
                config.CreateMap<ProductPhoto, DTOProductPhoto>().ReverseMap();
                config.CreateMap<DTOProductShoppingCart, ProductShoppingCart>().ReverseMap();
                config.CreateMap<DTOPurchaseStatus, PurchaseStatus>().ReverseMap();
                config.CreateMap<DTOShoppingCart, ShoppingCart>();
                config.CreateMap<ShoppingCart, DTOShoppingCart>()
                    .ForMember(dest => dest.SubTotal, source => source.MapFrom(opt => opt.CalculateSubTotal()));
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
