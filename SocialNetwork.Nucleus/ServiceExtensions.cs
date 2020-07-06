using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.EF.Repo;
using MediatR;
using SocialNetwork.Nucleus.Engine.Activity;
using System;
using SocialNetwork.Nucleus.Interfaces;

namespace SocialNetwork.Nucleus
{
    public static class ServiceExtensions
    {
        public static void ConfigureNucleusServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISessionUser, SessionUser>();
            services.ConfigureRepoServices(configuration);
            services.AddScoped<IValueEngine, ValueEngine>();
            services.AddScoped<IProfileReader, ProfileReader>();
            services.AddScoped<IUserActivityHelper, UserActivityHelper>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(List).Assembly);
        }
    }
}