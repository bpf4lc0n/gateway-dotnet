using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Gateway.Authorization.Users;

namespace Gateway.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string UserImage { get; set; }

        public string UserImageFileName { get; set; }
    }
}
