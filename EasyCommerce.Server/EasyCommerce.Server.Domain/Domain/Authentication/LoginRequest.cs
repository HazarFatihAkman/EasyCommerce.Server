using EasyCommerce.Server.Shared.Persistence.Entities;

namespace EasyCommerce.Server.Shared.Domain.Authentication;

public class LoginRequest : IMap<UserEntity>, IMap<CustomerEntity>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }

    void IMap<UserEntity>.Map(AutoMapper.Profile profile)
    {
       profile.CreateMap<UserEntity, LoginRequest>()
              .ForMember(dest => dest.Email, opt => opt.Condition(x => x.Email.IsNotNull()))
              .ForMember(dest => dest.Password, opt => opt.Condition(x => x.Password.IsNotNull()))
              .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
    void IMap<CustomerEntity>.Map(AutoMapper.Profile profile)
    {
        profile.CreateMap<CustomerEntity, LoginRequest>()
            .ForMember(dest => dest.Email, opt => opt.Condition(x => x.Email.IsNotNull()))
            .ForMember(dest => dest.Password, opt => opt.Condition(x => x.Password.IsNotNull()))
            .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
}
