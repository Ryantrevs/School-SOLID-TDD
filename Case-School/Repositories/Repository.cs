using Case_School.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_School.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Find(params object[] keyValues);
        Task<IEnumerable<TEntity>> FindAll();
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        Task Delete(TEntity entity);
        IQueryable<TEntity> Queryable();
        IRepository<TEntity> GetRepository();
    }
    
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class 
    {
        private readonly CaseSchoolContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._dbContext = unitOfWork.Context;
            this._dbSet = unitOfWork.Context.Set<TEntity>();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> Find(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public IRepository<TEntity> GetRepository()
        {
            return _unitOfWork.Repository<TEntity>();
        }
    }
}
