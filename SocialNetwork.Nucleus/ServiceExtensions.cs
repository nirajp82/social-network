using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.EF.Repo;
using MediatR;
using SocialNetwork.Nucleus.Engine.Activity;
using SocialNetwork.DataModel;
using SocialNetwork.Dto;

namespace SocialNetwork.Nucleus
{
    public static class ServiceExtensions
    {
        public static void ConfigureNucleusServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepoServices(configuration);
            services.AddScoped<IValueEngine, ValueEngine>();
            services.AddAutoMapper(typeof(List).Assembly, typeof(Activity).Assembly, typeof(ActivityDto).Assembly);
            services.AddMediatR(typeof(List).Assembly);
        }
    }
}