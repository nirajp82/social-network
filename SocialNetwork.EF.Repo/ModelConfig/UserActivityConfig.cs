using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    internal class UserActivityConfig : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            //Define composite key
            builder.HasKey(ua => new { ua.ActivityId, ua.AppUserId });

            //using following two one to many relationship, configure Many to Many relationship
            //One to Many Relationship
            builder.HasOne(ua => ua.AppUser)
                 .WithMany(u => u.Activities)
                 .HasForeignKey(ua => ua.AppUserId);
            //One to Many Relationship
            builder.HasOne(ua => ua.Activity)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(ua => ua.ActivityId);
        }
    }
}
