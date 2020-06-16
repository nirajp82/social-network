using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        private IActivityRepo _activityRepository { get; set; }
        private IValueRepo _valueRepository { get; set; }
        private IAppUserRepo _appUserRepo { get; set; }
        private IIdentityUserRepo _identityUserRepo { get; set; }
        private IUserActivityRepo _userActivityRepo { get; set; }

        private ApplicationContext _context { get; }
        #endregion


        #region Public Members
        //When the statement after the null-coalescing operator ?? is executed, the assigned value to IActivityRepo is actually returned by the propery.
        public IActivityRepo ActivityRepo => _activityRepository ?? (_activityRepository = new ActivityRepo(_context));
        public IValueRepo ValueRepo => _valueRepository ?? (_valueRepository = new ValueRepo(_context));
        public IAppUserRepo AppUserRepo => _appUserRepo ?? (_appUserRepo = new AppUserRepo(_context));
        public IIdentityUserRepo IdentityUserRepo => _identityUserRepo ?? (_identityUserRepo = new IdentityUserRepo(_context));
        public IUserActivityRepo UserActivityRepo => _userActivityRepo ?? (_userActivityRepo = new UserActivityRepo(_context));
        #endregion


        #region Constructors
        public UnitOfWork(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        #endregion


        #region Public Methods        
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}