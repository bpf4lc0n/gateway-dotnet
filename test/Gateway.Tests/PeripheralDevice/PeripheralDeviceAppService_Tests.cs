using System.Threading.Tasks;
using Shouldly;
using Xunit;
using PeripheralDeviceway.PeripheralDevices;
using Gateway.PeripheralDevices.Dto;
using Gateway.StaticNames;
using Gateway.Exceptions;
using Gateway.Gates;
using Gateway.Gates.Dto;
using System.Linq;

namespace Gateway.Tests.Sessions
{
    public class PeripheralDeviceAppService_Tests : GatewayTestBase
    {
        private readonly IPeripheralDeviceAppService _peripheralDeviceAppService;
        private readonly IGateAppService _GateAppService;

        public PeripheralDeviceAppService_Tests()
        {
            _peripheralDeviceAppService = Resolve<IPeripheralDeviceAppService>();
            _GateAppService = Resolve<IGateAppService>();
        }

        [Fact]
        public async Task Should_Raise_Error_When_Peripheral_Count_Exceed_Allow_Max()
        {
            var gates = await _GateAppService.GetAllAsync(new PagedGateResultRequestDto());
            var gate = gates.Items.FirstOrDefault(vl => vl.PeripheralDevices.Count >= 10);

            if (gate != null)
            {
                var value = new CreatePeripheralDeviceDto() {
                    Status = PeripheralDeviceStaticNames.Offline,
                    UID = 2,
                    Vendor = "Vendor",
                    GateId = gate.Id
                };

                try
                {
                    await _peripheralDeviceAppService.CreateAsync(value);
                }
                catch (System.Exception e)
                {
                    e.GetType().ShouldBeOfType(typeof(ToManyPeripheralException));
                }
             }
        }
    }
}
