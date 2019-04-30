using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhoneCall.API.Domain.Repositories;
using PhoneCall.API.Domain.Services;
using PhoneCall.API.Persistence.Contexts;
using PhoneCall.API.Persistence.Repositories;
using PhoneCall.API.Services;
using AutoMapper;
using PhoneCall.API.Mapping;

namespace PhoneCall.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set;}
        public IHostingEnvironment HostingEnvironment{get;private set;}
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            
        
            services.AddScoped<IUnitOfWork, UnitOfWork>();            
            services.AddDbContext<AppDbContext>(options=>{
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();      
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            

            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserService, UserService>();
            var mappingConfig = new MapperConfiguration(m=>{                
                m.AddProfile(new Mapping.MappingProfiles());
                              
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                            .AddJsonOptions(
                                options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }//https://localhost:5001/api/1.0/contacts/user/101            
            app.UseHttpsRedirection();
            app.UseCors(builder=> builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
