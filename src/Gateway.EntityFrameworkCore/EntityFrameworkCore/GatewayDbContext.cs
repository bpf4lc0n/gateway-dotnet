using Abp.Localization;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Gateway.Authorization.Roles;
using Gateway.Authorization.Users;
using Gateway.MultiTenancy;
using Gateway.Models;

namespace Gateway.EntityFrameworkCore
{
    public class GatewayDbContext : AbpZeroDbContext<Tenant, Role, User, GatewayDbContext>
    {       
        public DbSet<Gate> Gates { get; set; }
        public DbSet<PeripheralDevice> PeripheralDevices { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLanguageText>()
                .Property(p => p.Value)
                .HasMaxLength(100); 

            modelBuilder.Entity<Abp.Configuration.Setting>().Property(u => u.Value).HasMaxLength(2000000);
        }
    }
}
