﻿using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;

namespace SocialNetwork.EF.Repo.Infrastructure
{
    internal static class ModelBuilderExtensions
    {
        #region Public Method
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Value>()
            //     .HasData(new Value { Id = 1, Name = "Value 101" },
            //              new Value { Id = 2, Name = "Value 201" },
            //              new Value { Id = 3, Name = "Value 301" });

            //CreateActivities(modelBuilder);

            //CreateUser(modelBuilder);
        }
        #endregion


        #region Private Methods
        private static void CreateActivities(ModelBuilder modelBuilder)
        {
            IEnumerable<Activity> activities = new List<Activity> { new Activity
            {
                Title = "Past Activity 1",
                Date = DateTime.Now.AddMonths(-2),
                Description = "Activity 2 months ago",
                Category = "drinks",
                City = "London",
                Venue = "Pub",
            },
                  new Activity
                  {
                      Title = "Past Activity 2",
                      Date = DateTime.Now.AddMonths(-1),
                      Description = "Activity 1 month ago",
                      Category = "culture",
                      City = "Paris",
                      Venue = "Louvre",
                  },
                  new Activity
                  {
                      Title = "Future Activity 1",
                      Date = DateTime.Now.AddMonths(1),
                      Description = "Activity 1 month in future",
                      Category = "culture",
                      City = "London",
                      Venue = "Natural History Museum",
                  },
                 new Activity
                 {
                     Title = "Future Activity 2",
                     Date = DateTime.Now.AddMonths(2),
                     Description = "Activity 2 months in future",
                     Category = "music",
                     City = "London",
                     Venue = "O2 Arena",
                 },
                  new Activity
                  {
                      Title = "Future Activity 3",
                      Date = DateTime.Now.AddMonths(3),
                      Description = "Activity 3 months in future",
                      Category = "drinks",
                      City = "London",
                      Venue = "Another pub",
                  },
                 new Activity
                 {
                     Title = "Future Activity 4",
                     Date = DateTime.Now.AddMonths(4),
                     Description = "Activity 4 months in future",
                     Category = "drinks",
                     City = "London",
                     Venue = "Yet another pub",
                 },
                 new Activity
                 {
                     Title = "Future Activity 5",
                     Date = DateTime.Now.AddMonths(5),
                     Description = "Activity 5 months in future",
                     Category = "drinks",
                     City = "London",
                     Venue = "Just another pub",
                 },
                  new Activity
                  {
                      Title = "Future Activity 6",
                      Date = DateTime.Now.AddMonths(6),
                      Description = "Activity 6 months in future",
                      Category = "music",
                      City = "London",
                      Venue = "Roundhouse Camden",
                  },
                new Activity
                {
                    Title = "Future Activity 7",
                    Date = DateTime.Now.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "London",
                    Venue = "Somewhere on the Thames",
                },
                  new Activity
                  {
                      Title = "Future Activity 8",
                      Date = DateTime.Now.AddMonths(8),
                      Description = "Activity 8 months in future",
                      Category = "film",
                      City = "London",
                      Venue = "Cinema",
                  }
            };

            foreach (var item in activities)
            {
                item.Id = Guid.NewGuid();
                SeedAuditFields(item);
            }

            modelBuilder.Entity<Activity>()
                .HasData(activities);
        }

        private static void CreateUser(ModelBuilder modelBuilder)
        {
            //"Salt": "ywSeIEpKvoqze+9lvZaZ2g==" Password: "ZGM2ksCCUhC+7va1BBZ9bEw2H9tsCowdW9Pkflnp22U="
            //"Salt": "khOLQmG0hV6zkQNh8AyPPw==" Password: "dRV51KAkm6d9QggTJd3gDL+BBAg99/7lI564TQKjKTU="
            IEnumerable<AppUser> users = new List<AppUser> { new AppUser
            {
                 FirstName = "John",
                 LastName = "Doe",
                 Email = "JohnDoe@domain.com"
            },
             new AppUser
            {
                 FirstName = "Jane",
                 LastName = "Smith",
                 Email = "Jane.Smith@domain.com"
            },
             new AppUser
            {
                 FirstName = "Bruce",
                 LastName = "Lee",
                 Email = "Bruce.Lee@domain.com"
            }};

            foreach (var user in users)
            {
                user.Id = Guid.NewGuid();
                SeedAuditFields(user);
            }

            modelBuilder.Entity<AppUser>()
                .HasData(users);

            ICollection<IdentityUser> iUsers = CreateIdentityUsers(users);
            modelBuilder.Entity<IdentityUser>().HasData(iUsers);
        }

        private static ICollection<IdentityUser> CreateIdentityUsers(IEnumerable<AppUser> users)
        {
            ICollection<IdentityUser> iUsers = new List<IdentityUser>();
            int cnt = 0;
            foreach (var user in users)
            {
                cnt++;
                IdentityUser iUser = null;
                if (cnt == 1)
                {
                    iUser = new IdentityUser
                    {
                        Salt = "St0OnTE2Ju3Li9uSnlz/Mg==",
                        Passoword = "/8j7Y3aH1/NIcu5PWDxBaTftbv7kIhPN7IsIY+iDIZ0="
                    };
                }
                else if (cnt == 2)
                {
                    iUser = new IdentityUser
                    {
                        Salt = "f9/SzZwluz+xI51/VQQIzg==",
                        Passoword = "S10V+ChlwEm8VzgQIqvhHrUS65y7d9/E0AiYhKLwT0o="
                    };
                }
                else if (cnt == 3)
                {
                    iUser = new IdentityUser
                    {
                        Salt = "DEX8D+3HR9flD6NpGibucQ==",
                        Passoword = "5OekvvKMPp2M+O3Ts2/G912N9lCNqz412l1y8uHazZc="
                    };
                }

                iUser.Id = Guid.NewGuid();
                iUser.UserName = user.Email;
                iUser.AppUserId = user.Id;
                SeedAuditFields(iUser);

                iUsers.Add(iUser);
            }

            return iUsers;
        }

        private static void SeedAuditFields(IAuditModel audit)
        {
            audit.CreatedBy = "Seed";
            audit.UpdatedBy = "Seed";
            audit.CreatedDate = DateTime.Now;
            audit.UpdatedDate = DateTime.Now;
        }
        #endregion
    }
}