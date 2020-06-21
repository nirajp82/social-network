using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    internal class PhotoConfig : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            //using following two one to many relationship, configure Many to Many relationship
            //One to Many Relationship
            builder.HasOne(p => p.AppUser)
                 .WithMany(u => u.Photos)
                 .HasForeignKey(p => p.AppUserId);
        }
    }
}
