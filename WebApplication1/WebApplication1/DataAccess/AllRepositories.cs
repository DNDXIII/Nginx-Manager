namespace WebApplication1.DataAccess
{
    public class AllRepositories
    {
        public ServersDataAccess ServerRep { get; }
        public UpstreamDataAccess UpstreamRep { get; }
        public ProxyTypeDataAccess ProxyTypeRep { get; }
        public VirtualServerDataAccess VirtualServerRep { get; }
        public SSLDataAccess SSLRep { get; }
        public ApplicationDataAccess ApplicationRep { get; }
        public GeneralConfigDataAccess GeneralConfigRep { get; }
        public DeploymentServerDataAccess DeploymentServerRep { get; }
        public WhitelistDataAccess WhitelistRep { get; }




        public AllRepositories(ServersDataAccess s, UpstreamDataAccess u, ProxyTypeDataAccess p,
         VirtualServerDataAccess vs, SSLDataAccess ssl, ApplicationDataAccess app,  GeneralConfigDataAccess gc,
         DeploymentServerDataAccess ds, WhitelistDataAccess b)
        {
            ServerRep = s;
            UpstreamRep = u;
            ProxyTypeRep = p;
            VirtualServerRep = vs;
            SSLRep =ssl;
            ApplicationRep=app;
            GeneralConfigRep = gc;
            DeploymentServerRep = ds;
            WhitelistRep = b;
        }
    }
}
