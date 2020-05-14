using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        private IValueRepository _valueRepository { get; set; }
        private ApplicationContext _context { get; }
        #endregion


        #region Public Members
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