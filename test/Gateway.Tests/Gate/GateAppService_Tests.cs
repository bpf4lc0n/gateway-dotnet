using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Gateway.Gates;
using Gateway.Gates.Dto;
using Gateway.Validation;
using System;
using Gateway.Exceptions;

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

        [Fact]
        public void Should_Ipv4_IsValid_Work()
        {
            var ipv4_1 = "aa.1.1.1";
            var ipv4_2 = "999.1.1.1";
            var ipv4_3 = "1.1.1.1";

            ValidationHelper.IsIpv4(ipv4_1).ShouldBeFalse();
            ValidationHelper.IsIpv4(ipv4_2).ShouldBeFalse();
            ValidationHelper.IsIpv4(ipv4_3).ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Raise_Error_When_Ipv4_Is_Wrong()
        {
            var value = new CreateGateDto()
            {
                IPV4_address = "999.1.1.1",
                Human_readable_name = "Human_readable_name",
                Unique_serial_number = Guid.NewGuid().ToString()
            };

            try
            {
                await _GateAppService.CreateAsync(value);
            }
            catch (System.Exception e)
            {
                e.GetType().ShouldBeOfType(typeof(Ipv4InvalidException));
            }
        }
    }
}
