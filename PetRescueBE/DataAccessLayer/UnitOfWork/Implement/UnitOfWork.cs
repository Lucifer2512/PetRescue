﻿using DataAccessLayer.Repository.Implement;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAccessLayer.UnitOfWork.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext DbContext { get; private set; }
        private Dictionary<string, object> Repositories { get; }
        private IDbContextTransaction _transaction;
        private IsolationLevel? _isolationLevel;

        public UnitOfWork(DbFactory dbFactory)
        {
            DbContext = dbFactory.DbContext;
            Repositories = new Dictionary<string, dynamic>();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task StartNewTransactionIfNeeded()
        {
            if (_transaction == null)
            {
                _transaction = _isolationLevel.HasValue ?
                    await DbContext.Database.BeginTransactionAsync(_isolationLevel.GetValueOrDefault()) : await DbContext.Database.BeginTransactionAsync();
            }
        }

        public async Task BeginTransaction()
        {
            await StartNewTransactionIfNeeded();
        }

        public async Task CommitTransaction()
        {
            await DbContext.SaveChangesAsync();

            if (_transaction == null) return;
            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }


        public void Dispose()
        {
            if (DbContext == null)
                return;

            if (DbContext.Database.GetDbConnection().State == ConnectionState.Open)
            {
                DbContext.Database.GetDbConnection().Close();
            }
            DbContext.Dispose();

            DbContext = null;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);
            var typeName = type.Name;

            lock (Repositories)
            {
                if (Repositories.ContainsKey(typeName))
                {
                    return (IGenericRepository<TEntity>)Repositories[typeName];
                }

                var repository = new GenericRepository<TEntity>(DbContext);

                Repositories.Add(typeName, repository);
                return repository;
            }
        }
    }
}
