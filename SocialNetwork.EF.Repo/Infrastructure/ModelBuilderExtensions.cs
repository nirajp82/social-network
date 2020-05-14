using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;

namespace SocialNetwork.EF.Repo.Infrastructure
{
    internal static class ModelBuilderExtensions
    {
        #region Public Method
        internal static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Value>()
                 .HasData(new Value { Id = 1, Name = "Value 101" },
                          new Value { Id = 2, Name = "Value 201" },
                          new Value { Id = 3, Name = "Value 301" });
        }
        #endregion


        #region Private Methods
    
        #endregion
    }
}
