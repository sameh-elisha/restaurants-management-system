﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces.Services;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Services;
using Restaurants.Infrastructure.Services.Authorize;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");

            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());

            services.AddDbContext<RestaurantsDbContext>();

            services.AddIdentityApiEndpoints<ApplicationUser>()
               .AddRoles<IdentityRole>() // To Support the role claim in access token
                                         //.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>() // To Add More Attributes In Token
               .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IRatingsRepository, RatingsRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddScoped<IRatingAuthorizationService, RatingAuthorizationService>();
            services.AddScoped<IDishAuthorizationService, DishAuthorizationService>();
            services.AddScoped<ICustomerAuthorizationService, CustomerAuthorizationService>();
            services.AddScoped<ICategoryAuthorizationService, CategoryAuthorizationService>();
            services.AddScoped<IOrderAuthorizationService, OrderAuthorizationService>();


            services.AddHttpContextAccessor();

        }
    }
}
