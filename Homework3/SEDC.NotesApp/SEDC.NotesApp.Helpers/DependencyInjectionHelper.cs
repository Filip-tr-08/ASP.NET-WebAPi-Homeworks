using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Implementations;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            //services.AddTransient<IRepository<Note>, NotesRepository>(); //DI
            services.AddTransient<IRepository<User>, UsersRepository>(); //DI
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>(); //DI
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectAdoRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NotesAdoRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserAdoRepository(connectionString));
        }

        public static void InjectDapperRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepository<Note>>(x => new NotesDapperRepository(connectionString));
            services.AddTransient<IRepository<User>>(x => new UserDapperRepository(connectionString));
        }
    }
}
