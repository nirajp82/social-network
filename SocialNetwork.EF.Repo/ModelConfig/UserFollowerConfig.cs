using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.EF.Repo
{
    internal class UserFollowerConfig : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(uf => new { uf.UserId, uf.FollowerId });

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.Followings)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(uf => uf.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
