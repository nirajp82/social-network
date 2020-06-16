using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.EF.Repo;
using System;
using MediatR;
using SocialNetwork.Nucleus.Engine.Activity;

namespace SocialNetwork.Nucleus
{
    public static class ServiceExtensions
    {
        public static void ConfigureNucleusServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepoServices(configuration);
            services.AddScoped<IValueEngine, ValueEngine>();
            //TODO: Refactor
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(List).Assembly);
        }
    }
}