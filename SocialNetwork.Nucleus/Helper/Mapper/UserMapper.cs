using SocialNetwork.Dto;
using SocialNetwork.DataModel;
using SocialNetwork.Nucleus.User;
using SocialNetwork.Util;
using AutoMapper;

namespace SocialNetwork.Nucleus
{
    internal class UserMapper : BaseMapper
    {
        #region Constructor
        public UserMapper()
        {
            Map<AppUser, UserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.IdentityUser.UserName))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.IdentityUser.RefreshToken))
                .ForMember(dest => dest.Token, opt => opt.MapFrom<AppUserTokenResolver>());


            Map<AppUser, ProfileDto>(false)
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.IdentityUser != null ? src.IdentityUser.UserName : ""));

            Map<Edit.Command, AppUser>(false);

            Map<Register.Command, AppUser>(false)
                .ForMember(dest => dest.IdentityUser, opt => opt.MapFrom<IdentityUserResolver>());

            Map<IdentityUser, UserDto>(false)
              .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AppUser.DisplayName))
              .ForMember(dest => dest.Token, opt => opt.MapFrom<IdentityUserTokenResolver>())
              .ForMember(dest => dest.Image, opt => opt.MapFrom<IdentityUserPhotoResolver>());
        }
        #endregion


        #region Property Resolver
        private class IdentityUserResolver : IValueResolver<Register.Command, AppUser, IdentityUser>
        {
            #region Members
            private readonly ICryptoHelper _cryptoHelper;
            private readonly IJwtGenerator _jwtGenerator;
            #endregion


            #region Constructor
            public IdentityUserResolver(IJwtGenerator jwtGenerator, UtilFactory utilFactory)
            {
                _jwtGenerator = jwtGenerator;
                _cryptoHelper = utilFactory.CryptoHelper;
            }
            #endregion


            public IdentityUser Resolve(Register.Command source, AppUser dest, IdentityUser destMember, ResolutionContext context)
            {
                string salt = _cryptoHelper.CreateBase64Salt();
                return new IdentityUser
                {
                    UserName = source.UserName,
                    Salt = salt,
                    Passoword = _cryptoHelper.GenerateHash(source.Password, salt),
                    RefreshToken = _jwtGenerator.CreateRefreshToken(),
                    RefreshTokenExpiry = HelperFunc.GetCurrentDateTime().AddDays(30)
                };
            }
        }

        private class AppUserTokenResolver : IValueResolver<AppUser, UserDto, string>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            #endregion


            #region Constructor
            public AppUserTokenResolver(IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
            }
            #endregion


            public string Resolve(AppUser src, UserDto destination, string destMember, ResolutionContext context)
            {
                return _jwtGenerator.CreateToken(src.Id, src.IdentityUser.UserName);
            }
        }

        private class IdentityUserTokenResolver : IValueResolver<IdentityUser, UserDto, string>
        {
            #region Members
            private readonly IJwtGenerator _jwtGenerator;
            #endregion


            #region Constructor
            public IdentityUserTokenResolver(IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
            }
            #endregion


            public string Resolve(IdentityUser src, UserDto destination, string destMember, ResolutionContext context)
            {
                return _jwtGenerator.CreateToken(src.Id, src.UserName);
            }
        }

        private class IdentityUserPhotoResolver : IValueResolver<IdentityUser, UserDto, string>
        {
            #region Members
            private readonly IPhotoAccessor _photoAccessor;
            #endregion


            #region Constructor
            public IdentityUserPhotoResolver(IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
            }
            #endregion


            public string Resolve(IdentityUser src, UserDto dest, string destMember, ResolutionContext context)
            {
                return _photoAccessor.PreparePhotoUrl(src.AppUser.MainPhoto?.CloudFileName);
            }
        }
        #endregion
    }
}
