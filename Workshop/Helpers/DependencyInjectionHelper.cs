using DataAccess;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
   public static class DependencyInjectionHelper
    {
        public static void InjectionDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CinemaAppDbContext>(x => x.UseSqlServer(connectionString));
        }
        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IMoviesRepositorycs, MoviesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRepository<Director>, DirectorsRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMoviesServices, MoviesServices>(); 
            services.AddTransient<IDirectorsServices, DirectorServices>();
            services.AddTransient<IUsersServices, UserServices>();
        }
    }
    
}
