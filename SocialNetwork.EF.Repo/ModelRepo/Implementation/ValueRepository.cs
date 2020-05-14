using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal class ValueRepository : RepositoryBase<Value>, IValueRepository
    {
        #region Constructor
        public ValueRepository(ApplicationContext context) : base(context)
        {
        }

        #endregion


        #region Public Method
        public async Task<IEnumerable<Value>> FindAllAsync()
        {
            IQueryable<Value> result = base.FindAll(null);
            return await result.ToListAsync();
        }

        public async Task<Value> FindFirstAsync(long valueId)
        {
            return await base.FindFirstAsync(e => e.Id == valueId);
        }

        public async Task DeleteAsync(long valueId)
        {
            await base.DeleteAsync(e => e.Id == valueId);
        }
        #endregion
    }
}