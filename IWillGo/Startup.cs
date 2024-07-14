using IWillGo.DataAccess;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Services;
using IWillGo.Services.Interfaces;
using IWillGo.Authentication;
using IWillGo.Authentication.Interfaces;
using IWillGo.Authentication;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IWillGo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider serviceProvider { get; }
        public IConfiguration Configuration { get; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Setting configuration for protected web api
            // Add JWT authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtOptions:SigningKey"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Creating policies that wraps the authorization requirements
            services.AddAuthorization();

            services.AddControllers();            
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IWillGo API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.CustomSchemaIds(i => i.FullName);
            });

            services.AddTransient<IDbConnection, SqlConnection>(db => new SqlConnection(Configuration.GetConnectionString("Prod")));


            services.AddCors(options => options.AddPolicy("default",
               builder => builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader())
           );

            services.AddHttpClient();

            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IOpportunitiesService, OpportunitiesService>();
            services.AddScoped<IJwtAuthenticationManager>(sp => new JwtAuthenticationManager(Configuration["JwtOptions:SigningKey"], sp.GetRequiredService<IMemberService>()));


            //Repos
            services.AddScoped<IGetMemberRepo, MemberGetRepo>();
            services.AddScoped<ISaveMemberRepo, MemberSaveRepo>();
            services.AddScoped<IGetOpportunityRepo, GetOpportunityRepo>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IGetMemberRepo memberGetRepo, ISaveMemberRepo memberSaveRepo, IGetOpportunityRepo opportunityRepo) //Add repo
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Phatboi Studios - IWillGo API V1");
            });
            app.UseCors("default");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
        }
    }
}
