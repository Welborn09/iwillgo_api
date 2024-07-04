using IWillGo.DataAccess;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Services;
using IWillGo.Services.Interfaces;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using System.Data;

namespace IWillGo
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
            // Setting configuration for protected web api
            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration);*/

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

            services.AddScoped<IOpportunitiesService, OpportunitiesService>();
            services.AddScoped<IMemberService, MemberService>();

            //Repos
            services.AddScoped<IGetMemberRepo, MemberGetRepo>();
            services.AddScoped<ISaveMemberRepo, MemberSaveRepo>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IGetMemberRepo memberGetRepo, ISaveMemberRepo memberSaveRepo) //Add repo
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
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("default");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
        }
    }
}
