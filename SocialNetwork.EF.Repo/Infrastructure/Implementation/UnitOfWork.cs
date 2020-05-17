using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        private IActivityRepository _activityRepository { get; set; }
        private IValueRepository _valueRepository { get; set; }
        private ApplicationContext _context { get; }
        #endregion


        #region Public Members
        public IActivityRepository ActivityRepository
        {
            get
            {
                if (_activityRepository == null)
                    _activityRepository = new ActivityRepository(_context);

                return _activityRepository;
            }
        }

        public IValueRepository ValueRepository
        {
            get
            {
                if (_valueRepository == null)
                    _valueRepository = new ValueRepository(_context);

                return _valueRepository;
            }
        }
        #endregion


        #region Constructors
        public UnitOfWork(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        #endregion


        #region Public Methods        
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}