namespace Gateway.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly GatewayDbContext _context;

        public InitialHostDbBuilder(GatewayDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
