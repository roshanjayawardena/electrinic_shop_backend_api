using Electronic_Application.Contracts.Infastructure;
using Electronic_Application.Contracts.Infastructure.Auth;
using Electronic_Application.Contracts.Persistence;
using Electronic_Application.Models.Email;
using Electronic_Infastructure.FireBaseStorage;
using Electronic_Infastructure.Identity;
using Electronic_Infastructure.Mail;
using Electronic_Infastructure.Persistence;
using Electronic_Infastructure.Repositories;
using Electronic_Infastructure.Seed;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Electronic_Infastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static async Task<IServiceCollection> ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add dbcontext
            services.AddDbContext<ElectronicContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ElectronicConnectionString")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                           .GetBytes(configuration.GetSection("JWT:Secret").Value)),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ClockSkew = TimeSpan.Zero
                   };
               });

            //// Add Authentication and JwtBearer
            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //       // options.SaveToken = true;
            //       // options.RequireHttpsMetadata = false;
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateIssuerSigningKey = true,
            //            //ValidIssuer = configuration.GetSection("JWT:ValidIssuer").Value,
            //            //ValidAudience = configuration.GetSection("JWT:ValidAudience").Value,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Secret").Value)),
            //           // ClockSkew = TimeSpan.Zero,

            //        };
            //    });

            // Add Identity
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ElectronicContext>()
                .AddDefaultTokenProviders();


            // Config Identity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            });

            // Register the RoleSeeder
            services.AddScoped<ElectronicContextSeed>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            // Seed roles
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var roleSeeder = scope.ServiceProvider.GetRequiredService<ElectronicContextSeed>();
                await roleSeeder.SeedRolesAsync();
                await roleSeeder.SeedAdminUserAsync();
            }

            services.AddScoped<IApplicationDBContext, ElectronicContext>();
            services.AddTransient(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IShippingDetailRepository, ShippingDetailRepository>();

            // Register sendgrid service
            //services.AddSendGrid(options =>
            //{
            //    options.ApiKey = configuration.GetSection("SendGridEmailSettings").GetValue<string>("APIKey");
            //});

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            //services.AddTransient<IBlobStorageService, BlobStorageService>();

            // Provide the path to your Firebase service account JSON file
            var serviceAccountPath = System.IO.Path.Combine(AppContext.BaseDirectory + "./FireBaseStorage/electronic-shop-9fe85-firebase-adminsdk-q7764-b4fc93fe64.json");

            // Configure GoogleCredential using the service account key file
            GoogleCredential credential = GoogleCredential.FromFile(serviceAccountPath);

            // Configure FirebaseApp with the credential
            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential,
            });

            // Use the credential to authenticate with Google Cloud Storage
            //var storageClient = StorageClient.Create(credential);

            services.AddSingleton<IFirebaseStorageService>(s => new FirebaseStorageService(StorageClient.Create(credential)));

            return services;          
        }

    }
}
