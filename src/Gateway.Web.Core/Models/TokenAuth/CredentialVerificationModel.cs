using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace Gateway.Models.TokenAuth
{
    public class CredentialVerificationModel
    {
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}
