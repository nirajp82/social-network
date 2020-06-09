using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    internal class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(i => i.IdentityUser)
                .WithOne(a => a.AppUser)
                .HasForeignKey<IdentityUser>(i => i.AppUserId);
        }
    }
}
