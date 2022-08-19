using EasyCommerce.Server.Shared.Common.Options;

namespace EasyCommerce.Server.Shared.Options;

public class UserOptions : IOption
{
    public string Position => "User";
    public string ApiKey { get; set; }
    public int AddMonths { get; set; }

}
