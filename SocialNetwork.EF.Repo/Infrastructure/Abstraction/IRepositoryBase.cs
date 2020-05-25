using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal interface IRepositoryBase<TEntity> where TEntity : IBaseModel
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll(IEnumerable<string> includes = null);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null,
            bool isNoTracking = true);

        Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate,
            IEnumerable<string> includes = null,
            CancellationToken cancellationToken = default);

        Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);
    }
}