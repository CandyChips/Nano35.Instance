using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Services.AppStart.Configure
{
    public class EntityFrameworkServiceConstruct
    {
        private readonly string _dbServer;
        private readonly string _catalog;
        private readonly string _login;
        private readonly string _password;

        public EntityFrameworkServiceConstruct(
            string dbServer, 
            string catalog, 
            string login,
            string password)
        {
            _dbServer = dbServer;
            _catalog = catalog;
            _login = login;
            _password = password;
        }
        
        public void Register(IServiceCollection services) 
        {
            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer($"server={_dbServer}; Initial Catalog={_catalog}; User id={_login}; Password={_password};"));
        }
    }
}