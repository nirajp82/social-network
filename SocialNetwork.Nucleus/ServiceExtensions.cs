using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.EF.Repo;
using System;
using MediatR;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Nucleus
{
    public static class ServiceExtensions
    {
        public static void ConfigureNucleusServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureInfrastructureServices(configuration);
            services.ConfigureRepoServices(configuration);
            services.AddScoped<IValueEngine, ValueEngine>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(List).Assembly);
        }
    }
}