using System;
using System.Linq;
using Gateway.Models;
using Gateway.StaticNames;
using Microsoft.EntityFrameworkCore;

namespace Gateway.EntityFrameworkCore.Seed.CustomBuilders
{
    /// <summary>
    ///     The default PeripheralDevice builder.
    /// </summary>
    /// <typeparam name="T">
    ///     The context type.
    /// </typeparam>
    public class DefaultGatePeripheralDeviceBuilder
    {
        /// <summary>
        ///     The context.
        /// </summary>
        private readonly GatewayDbContext context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultPeripheralDeviceBuilder{T}" /> class.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        public DefaultGatePeripheralDeviceBuilder(GatewayDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///     The create.
        /// </summary>
        public void Create()
        {
            TryAddGates("Gate0", "192.168.43.253");
            TryAddGates("Gate1", "192.168.43.254");
            TryAddGates("Gate2", "192.168.43.255");
            TryAddGates("Gate3", "192.168.43.256");
            TryAddGates("Gate4", "192.168.43.257");
            TryAddGates("Gate5", "192.168.43.258");
            context.SaveChanges();

            for (int i = 0; i < 60; i++)
            {
                var state = (i % 2) == 0;
                TryAddPeripheralDevices(i, state ? PeripheralDeviceStaticNames.Online : PeripheralDeviceStaticNames.Offline);
            }
            context.SaveChanges();
        }

        private void TryAddGates(string name, string ipv4)
        {
            if (!context.Gates.IgnoreQueryFilters().Any(e => e.Human_readable_name == name))
            { 
                context.Gates.Add(new Gate
                {
                    Unique_serial_number = Guid.NewGuid().ToString(),
                    Human_readable_name = name,
                    IPV4_address = ipv4
                }) ;
            }
        }

        private void TryAddPeripheralDevices(int uid, string state)
        {
            var _random = new Random();
            var GateName = string.Concat("Gate", _random.Next(5));
            var Gate = context.Gates.SingleOrDefault(vl => vl.Human_readable_name == GateName);

            if (Gate == null)
                return;

            if (!context.PeripheralDevices.IgnoreQueryFilters().Any(e => e.UID == uid))
            {
                context.PeripheralDevices.Add(new PeripheralDevice
                {
                    UID = uid,
                    Status = state,
                    DateCreated = DateTime.Now,
                    Vendor = string.Concat("Vendor", _random.Next(5)),
                    GateId = Gate.Id
                }) ;
            }
        }
    }
}
