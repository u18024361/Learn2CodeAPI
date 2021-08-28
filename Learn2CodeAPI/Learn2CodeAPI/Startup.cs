using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emailservice;
using Learn2CodeAPI.Data;
using Learn2CodeAPI.Data.Mapper;
using Learn2CodeAPI.IRepository.Generic;
using Learn2CodeAPI.IRepository.IRepositoryAdmin;
using Learn2CodeAPI.IRepository.IRepositoryLogin;
using Learn2CodeAPI.IRepository.IRepositoryStudent;
using Learn2CodeAPI.IRepository.IRepositoryTutor;
using Learn2CodeAPI.JwtFeatures;
using Learn2CodeAPI.Models.Login.Identity;
using Learn2CodeAPI.Repository.Generic;
using Learn2CodeAPI.Repository.RepositoryAdmin;
using Learn2CodeAPI.Repository.RepositoryLogin;
using Learn2CodeAPI.Repository.RepositoryStudent;
using Learn2CodeAPI.Repository.RepositoryTutor;
using Learn2CodeAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Twilio.Clients;

namespace Learn2CodeAPI
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
            services.AddControllersWithViews().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

          
                         

            services.AddControllers();
            services.AddScoped<IStudent, StudentRepository>();
            services.AddScoped<ITutor, TutorRepo>();
            services.AddScoped<IAdmin, AdminRepo>();
            services.AddScoped<ILogin, LoginRepo>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddHttpClient<ITwilioRestClient, TwilioClient>();
            var emailConfig = Configuration
           .GetSection("EmailConfiguration")
           .Get<Emailconfiguration>();
            services.AddSingleton(emailConfig);

            services.AddScoped(typeof(IGenRepository<>), typeof(GenRepository<>));
            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                     opt.TokenLifespan = TimeSpan.FromHours(2));

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
            services.AddScoped<JwtHandler>();


            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Learn2CodeMapper));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:44393",
                                        "http://localhost:4200",
                                        "http://localhost:8100"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext context, UserManager<AppUser> userManeger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            SeedHelpers.SeedDb(context, userManeger);
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
