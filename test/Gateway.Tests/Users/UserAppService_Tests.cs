using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using Gateway.Users;
using Gateway.Users.Dto;
using Gateway.Sessions;

namespace Gateway.Tests.Users
{
    public class UserAppService_Tests : GatewayTestBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ISessionAppService _sessionAppService;

        public UserAppService_Tests()
        {
            _userAppService = Resolve<IUserAppService>();
            _sessionAppService = Resolve<ISessionAppService>();
        }


        [Fact]
        public async Task CreateUser_Test()
        {
            // Act
            await _userAppService.CreateAsync(
                new UserDto
                {
                    EmailAddress = "john@volosoft.com",
                    IsActive = true,
                    Name = "John",
                    Surname = "Nash",

                    UserName = "john.nash"
                });

            await UsingDbContextAsync(async context =>
            {
                var johnNashUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "john.nash");
                johnNashUser.ShouldNotBeNull();
            });
        }
    }
}
