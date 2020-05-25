using Microsoft.EntityFrameworkCore;
using SocialNetwork.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.EF.Repo
{
    internal abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IBaseModel
    {
        #region Member
        protected ApplicationContext _context { get; }
        #endregion


        #region Constuctor
        protected RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }
        #endregion


        #region Public Methods
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            //_context.Set<T>().Update(entity);
        }


        public void Delete(TEntity entity)
        {
            if (entity != null)
                _context.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<TEntity> list = await Find(predicate).ToListAsync(cancellationToken);
            if (list?.Any() == true)
            {
                foreach (var item in list)
                    Delete(item);
            }
        }


        public IQueryable<TEntity> GetAll(IEnumerable<string> includes = null)
        {
            return Find(null, includes);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Find(predicate, null);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes, Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null, bool isNoTracking = true)
        {
            IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (predicate != null)
                queryable = queryable.Where(predicate);

            if (includes?.Any() == true)
                queryable = includes.Aggregate(queryable, (current, inc) => current.Include(inc));

            if (orderBy != null)
                queryable = orderBy(queryable);

            if (skip.GetValueOrDefault() > 0)
                queryable = queryable.Skip(skip.Value);

            if (take.GetValueOrDefault() > 0)
                queryable = queryable.Take(take.Value);

            if (isNoTracking)
                queryable = queryable.AsNoTracking();

            return queryable;
        }

        public async Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> predicate,
            IEnumerable<string> includes = null,
            CancellationToken cancellationToken = default)
        {
            return await Find(predicate, includes).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await Find(predicate, null).AnyAsync(cancellationToken);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await Find(predicate, null).LongCountAsync(cancellationToken);
        }
        #endregion
    }
}