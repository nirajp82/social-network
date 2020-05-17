using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo.Infrastructure;
using System.Linq;

namespace SocialNetwork.EF.Repo
{
    public class ApplicationContext : DbContext
    {
        #region Members
        public DbSet<Value> Values { get; set; }
        public DbSet<Activity> Activities { get; set; }
        #endregion


        #region Constructor
        public ApplicationContext(DbContextOptions dbContextOptions) :
            base(dbContextOptions)
        {
        }
        #endregion


        #region Public Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.ClrType == typeof(string)))
            {
                int? maxLen = property.GetMaxLength();
                maxLen = maxLen ?? 1;
                if (property.GetColumnType() == null)
                    property.SetColumnType($"VARCHAR({maxLen})");
            }
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
