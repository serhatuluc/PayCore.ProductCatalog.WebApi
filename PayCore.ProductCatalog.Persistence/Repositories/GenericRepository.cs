using NHibernate;
using NHibernate.Linq;
using PayCore.ProductCatalog.Application.Interfaces.Log;
using PayCore.ProductCatalog.Application.Interfaces.Repositories;
using PayCore.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PayCore.ProductCatalog.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseEntity
    {
        private  readonly ILoggerManager Logger;
        private readonly ISession session;
        private ITransaction transaction;

        public GenericRepository(ISession session,ILoggerManager Logger)
        {
            this.session = session;
            this.Logger = Logger;
        }

        public async Task Create(Entity entity)
        {
            try
            {
                transaction = session.BeginTransaction();
                await session.SaveAsync(entity);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var error = ex.Message;
                Logger.LogError(ex,"Insert Error");
            }
            finally
            {
                session.Dispose();
            };
        }

        public async Task Delete(Entity entity)
        {
            try
            {
                transaction = session.BeginTransaction();
                await session.DeleteAsync(entity);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {

                Logger.LogError( ex,"Delete Error");
            }
            finally
            {
                session.Dispose();
            };
        }
        #nullable enable
        public async Task<IEnumerable<Entity>> GetAll(Expression<Func<Entity, bool>> expression)
        {
            if(expression is null)
            {
                return await session.Query<Entity>().ToListAsync();
            }
            return await session.Query<Entity>().Where(expression).ToListAsync();

        }
        public async Task<IEnumerable<Entity>> GetAll()
        {
            return await session.Query<Entity>().ToListAsync();
        }

        public async Task<Entity> GetById(int id)
        {
            return await session.Query<Entity>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Entity entity)
        {
            try
            {
                transaction = session.BeginTransaction();
                await session.UpdateAsync(entity);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Logger.LogError(ex, "Update Error");
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}
