using Case_School.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Case_School.Repositories
{
    public interface IUnitOfWork
    {
        public CaseSchoolContext Context { get; }
        Task Commit();
        IRepository<TEntity> Repository<TEntity>();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CaseSchoolContext _dbContext;
        public CaseSchoolContext Context => _dbContext;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(CaseSchoolContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>(); var type = typeof(TEntity).Name; if (_repositories.ContainsKey(type))
                return (IRepository<TEntity>)_repositories[type]; var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), this)
            );
            return _repositories[type];
        }
    }
}
