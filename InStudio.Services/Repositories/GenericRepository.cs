﻿using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using InStudio.Common.Types;
using InStudio.Common;
using InStudio.Data;
using InStudio.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace InStudio.Services.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public T GetSingleByCriteria(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                int count = includes.Length;
                for (int index = 0; index < count; index++)
                {
                    query = query.Include(includes[index]);
                }
            }

            return query.AsNoTracking().Where(criteria).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int pageIndex, int pageSize)
        {
            return await _context.Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.AnyAsync(criteria);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public async Task<PagedReadOnlyCollection<TResult>> GetPagedWithFilterAndProjectToAsync<TResult>(Expression<Func<T, bool>> criteria, PageableParams pagingParams, SortParameter sortParamters)
           where TResult : class
        {
            var query = _context.Set<T>().Where(criteria);

            var totalCount = await query.LongCountAsync();
            if (totalCount == 0)
            {
                return new PagedReadOnlyCollection<TResult>(new List<TResult>(), totalCount);
            }

            if (!string.IsNullOrEmpty(sortParamters.SortBy))
            {
                query = query.OrderBy($"{sortParamters.SortBy} {sortParamters.SortDirection}");
            }

            var pagedOrdered = query.Skip((pagingParams.Page - 1) * pagingParams.Size).Take(pagingParams.Size);
            var list = await pagedOrdered.ProjectToType<TResult>().ToListAsync();

            return new PagedReadOnlyCollection<TResult>(list, totalCount);
        }

        public async Task<PagedReadOnlyCollection<TResult>> GetPagedWithFilterAndProjectToAsync<TResult>(Expression<Func<T, bool>> criteria, PageableParams pagingParams, SortParameter sortParameters, params string[] includes) where TResult : class
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(criteria);

            var totalCount = await query.LongCountAsync();
            if (totalCount == 0)
            {
                return new PagedReadOnlyCollection<TResult>(new List<TResult>(), totalCount);
            }

            if (!string.IsNullOrEmpty(sortParameters.SortBy))
            {
                query = query.OrderBy($"{sortParameters.SortBy} {sortParameters.SortDirection}");
            }

            var pagedOrdered = query.Skip((pagingParams.Page - 1) * pagingParams.Size).Take(pagingParams.Size);
            var list = await pagedOrdered.ProjectToType<TResult>().ToListAsync();

            return new PagedReadOnlyCollection<TResult>(list, totalCount);
        }

        public async Task<T> GetSingleByCriteriaAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking()?.Where(criteria).FirstOrDefaultAsync();
        }
        public async Task<List<T>> ListByCriteriaAsync(Expression<Func<T, bool>> criteria, params string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking().Where(criteria).ToListAsync();
        }
        public async Task<int> CountByCriteriaAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _context.Set<T>();
            return await query.Where(criteria).CountAsync();
        }

    }
}
