using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Services.AppStart.Configure
{
    public static class EntityFrameworkServiceConstructor 
    {
        public static void Construct(IServiceCollection services) 
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer("server=192.168.100.120; Initial Catalog=Nano35.Instance.DB; User id=sa; Password=Cerber666;"));
        }
    }
}