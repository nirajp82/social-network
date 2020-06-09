using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using SocialNetwork.EF.Repo.Infrastructure;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    public class ApplicationContext : DbContext
    {
        #region Members
        public DbSet<Value> Values { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<IdentityUser> IdentityUser { get; set; }
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

            modelBuilder.ApplyConfiguration(new AppUserConfig());

            //Set string type to VARCHAR
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                        .SelectMany(t => t.GetProperties())
                        .Where(p => p.ClrType == typeof(string)))
            {
                int? maxLen = property.GetMaxLength();
                maxLen = maxLen ?? 1;
                if (property.GetColumnType() == null)
                    property.SetColumnType($"VARCHAR({maxLen})");
            }

            //Keep Singular Table Name
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditValues();
            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion


        #region Private Method
        private void SetAuditValues()
        {
            var entries = ChangeTracker.Entries()
                                        .Where(e => e.Entity is IAuditModel &&
                                        (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IAuditModel)entityEntry.Entity).UpdatedDate = DateTime.Now;
                if (entityEntry.State == EntityState.Added)
                    ((IAuditModel)entityEntry.Entity).CreatedDate = DateTime.Now;
                else 
                {
                    entityEntry.Property(nameof(IAuditModel.CreatedDate)).IsModified = false;
                    entityEntry.Property(nameof(IAuditModel.CreatedBy)).IsModified = false;
                }
            }
        }
        #endregion
    }
}
