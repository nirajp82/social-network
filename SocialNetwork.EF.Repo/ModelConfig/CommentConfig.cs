using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo
{
    internal class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(e => e.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder.HasOne(e => e.Activity)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ActivityId);
        }
    }
}