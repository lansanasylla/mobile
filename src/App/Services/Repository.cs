﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bit.App.Abstractions;
using SQLite;

namespace Bit.App.Services
{
    public abstract class Repository<T, TId>
        where TId : IEquatable<TId> 
        where T : class, IDataObject<TId>, new()
    {
        public Repository(ISqlService sqlService)
        {
            Connection = sqlService.GetConnection();
        }

        protected SQLiteConnection Connection { get; private set; }

        protected virtual Task<T> GetByIdAsync(TId id)
        {
            return Task.FromResult(Connection.Get<T>(id));
        }

        protected virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Connection.Table<T>().Cast<T>());
        }

        protected virtual Task CreateAsync(T obj)
        {
            Connection.Insert(obj);
            return Task.FromResult(0);
        }

        protected virtual Task ReplaceAsync(T obj)
        {
            Connection.Update(obj);
            return Task.FromResult(0);
        }

        protected virtual Task DeleteAsync(T obj)
        {
            Connection.Delete<T>(obj.Id);
            return Task.FromResult(0);
        }
    }
}