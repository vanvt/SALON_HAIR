﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SALON_HAIR_CORE.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        bool Any<Tsoure>(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> SearchAllFileds(string keyword);
        //IEnumerable<T> SearchAllFileds(string keyword);
        IQueryable<T> Paging(IQueryable<T> query, int currentPage, int rowPerPage);
        Task<T> FindAsync(params object[] keyValues);
        T Find(params object[] keyValues);
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> EditAsync(T entity);
        Task<int> SaveAsync();
        Task<int> AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        Task<int> DeleteRangeAsync(IEnumerable<T> entities);
        Task<int> EditRangeAsync(IEnumerable<T> entities);
        IQueryable<T> SearchAllFileds(string keyword, string field, string type);
        EntityEntry<T> Entry(T entity);
        T LoadAllReference(T entity);
        T LoadAllCollecttion(T entity);
        IQueryable<T> LoadAllInclude(IQueryable<T> entity);
        IQueryable<T> LoadAllCollecttion(IQueryable<T> entity);        
        Task<IEnumerable<T>> LoadAllIncludeEnumAsync(IQueryable<T> rs);
        IQueryable<T> LoadAllCollecttion(IQueryable<T> rs, params string[] excludes);
        IQueryable<T> LoadMany2Many(IQueryable<T> rs, params string[] excludes);
        IQueryable<T> LoadAllInclude(IQueryable<T> rs, params string[] excludes);
        T LoadAllCollecttion(T entity, params string[] excludes);
        T LoadAllReference(T entity, params string[] excludes);
        void RemoveLogic<TDel>(long id);
        void RemoveLogic<TDel>(long[] ids);
        void RemovePhysical<TDel>(long id);
        void RemovePhysical<TDel>(long[] ids);

        Task RemoveLogicAsync<TDel>(long id);
        Task RemoveLogicAsync<TDel>(long[] ids);
        Task RemovePhysicalAsync<TDel>(long id);
        Task RemovePhysicalAsync<TDel>(long[] ids);
        void RemoveLogic<TDel, Tkey>(long[] ids, Expression<Func<TDel, Tkey>> predicate);
        IQueryable<T> GetExtenQuery<Tsoure>(Expression<Func<T, bool>> predicate, IQueryable<T> rs);

    }
}
