using Business.Helpers;
using Business.Interface;
using Business.ModelDTO;
using Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Business.StartupServices
{
    public static class BusinessService
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingInitializer));

            services.AddScoped<IManagerServiceAsync, ManagerServiceAsync>();
            services.AddScoped<IOrganizationServiceAsync, OrganizationServiceAsync>();
            services.AddScoped<IRegionServiceAsync, RegionServiceAsync>();
            services.AddScoped<ICheckServiceAsync, CheckServiceAsync>();
            services.AddScoped<IMessageServiceAsync, MessageServiceAsync>();

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
