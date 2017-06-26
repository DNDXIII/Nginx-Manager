using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class AllRepositories
    {
        public IRepository<Server> ServerRep { get; }
        public IRepository<Upstream> UpstreamRep { get; }
        public IRepository<ProxyType> ProxyTypeRep { get; }
        public IRepository<VirtualServer> VirtualServerRep { get; }
        public IRepository<SSL> SSLRep { get; }
        public IRepository<Application> ApplicationRep { get; }
        public IRepository<Location> LocationRep { get; }
        public IRepository<GeneralConfig> GeneralConfigRep { get; }
        public IRepository<DeploymentServer> DeploymentServerRep { get; }
        public IRepository<Blacklist> BlacklistRep { get; }




        public AllRepositories(ServersDataAccess s, UpstreamDataAccess u, ProxyTypeDataAccess p,
         VirtualServerDataAccess vs, SSLDataAccess ssl, ApplicationDataAccess app, LocationsDataAccess l, GeneralConfigDataAccess gc,
         DeploymentServerDataAccess ds, BlacklistDataAccess b)
        {
            ServerRep = s;
            UpstreamRep = u;
            ProxyTypeRep = p;
            VirtualServerRep = vs;
            SSLRep =ssl;
            ApplicationRep=app;
            LocationRep = l;
            GeneralConfigRep = gc;
            DeploymentServerRep = ds;
            BlacklistRep = b;
        }
    }
}
