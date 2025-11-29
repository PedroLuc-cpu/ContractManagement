using Microsoft.AspNetCore.Identity;

namespace ContractManagement.Infrastructure.Identity
{
    public enum StatusUserEnum
    {
        Offline = 0,
        Online = 1,
    }

    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        private StatusUserEnum statusUserEnum;

        public StatusUserEnum StatusUser
        {
            get { return statusUserEnum; }
            set
            {
                statusUserEnum = value;
                DescricaoStatus = value switch { StatusUserEnum.Online => "On-Line", StatusUserEnum.Offline => "Off-Line", _ => "Off-Line", };

            }
        }
        public string DescricaoStatus { get; set; } = string.Empty;
    }
}
