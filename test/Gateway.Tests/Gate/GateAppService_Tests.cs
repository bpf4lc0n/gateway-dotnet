using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Gateway.Gates;
using Gateway.Gates.Dto;

namespace Gateway.Tests.Gates
{
    public class GateAppService_Tests : GatewayTestBase
    {
        private readonly IGateAppService _GateAppService;

        public GateAppService_Tests()
        {
            _GateAppService = Resolve<IGateAppService>();
        }

        [Fact]
        public async Task Should_Have_Equeal_Or_Less_Than_10_Peripheral()
        {
            var request = new PagedGateResultRequestDto();

            // Act
            var output = await _GateAppService.GetAllAsync(request);

            // Assert
            foreach (var item in output.Items)
            {
                item.PeripheralDevices.Count.ShouldBeLessThanOrEqualTo(10);
            }
        }

        [Fact]
        public async Task Should_Ipv4_Fields_Are_Valid()
        {
            var request = new PagedGateResultRequestDto();

            // Act
            var output = await _GateAppService.GetAllAsync(request);

            // Assert
            foreach (var item in output.Items)
            {
                var arr = item.IPV4_address.Split('.');
                arr.Length.ShouldBe(4);
                foreach (var part in arr)
                {
                    int.TryParse(part, out var partValue).ShouldBeTrue();
                    partValue.ShouldBeGreaterThanOrEqualTo(0);
                    partValue.ShouldBeLessThanOrEqualTo(255);
                }
            }
        }
    }
}
