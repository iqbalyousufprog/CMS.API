using CMS.API.Data;
using CMS.API.Model.Domain;
using CMS.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CMS.API.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity, new()
    {
        protected readonly CMSDbContext dbContext;
        private readonly DbSet<T> dbSet;
        
        public BaseRepository(CMSDbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            await dbSet.AddAsync(model);
            await dbContext.SaveChangesAsync();
           
            return model;
        }

        public async Task<T?> DeleteAsync(int id)
        {
            var exsitingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (exsitingEntity == null)
                return null;

            dbSet.Remove(exsitingEntity);
            await dbContext.SaveChangesAsync();

            return exsitingEntity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IQueryable<T> GetQuerable()
        {
            return dbSet.AsQueryable<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(f => f.Id == id);
        }

        public virtual async Task<T?> UpdateAsync(T model)
        {
            var exsitingEntity = await dbSet.FirstOrDefaultAsync(f => f.Id == model.Id);

            if (exsitingEntity != null)
            {
                dbSet.Entry(exsitingEntity).State = EntityState.Modified;
                dbSet.Entry(exsitingEntity).CurrentValues.SetValues(model);
                await dbContext.SaveChangesAsync();
                return model;
            }

            return null;
        }

        protected async Task<T?> UpdateAsync(T model, params Expression<Func<T, object>>[] includes)
        {
            var existingEntity = await dbSet
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(f => f.Id == model.Id);

            if (existingEntity == null)
                return null;

            // Update the entity properties
            dbSet.Entry(existingEntity).State = EntityState.Modified;
            dbSet.Entry(existingEntity).CurrentValues.SetValues(model);

            // Update related entities
            foreach (var include in includes)
            {
                var propertyName = CommonExtension.GetMemberName(include);

                if (propertyName == null)
                    continue;

                var entityType = include.ReturnType.GenericTypeArguments.FirstOrDefault();

                if (entityType != null)
                {
                    var collectionType = typeof(ICollection<>).MakeGenericType(entityType);
                    var newCollection = Activator.CreateInstance(collectionType);

                    // Set the new collection as the CurrentValue of the reference
                    dbSet.Entry(existingEntity).Reference(propertyName).CurrentValue = newCollection;

                    // Load the reference to ensure it is tracked by the context
                    dbSet.Entry(existingEntity).Reference(propertyName).Load();

                    // Set the navigation property value from the model
                    var navigationPropertyValue = include.Compile().Invoke(existingEntity);
                    dbSet.Entry(existingEntity).Reference(propertyName)?.TargetEntry?.CurrentValues?.SetValues(navigationPropertyValue);
                }
            }

            await dbContext.SaveChangesAsync();

            return model;
        }

        protected async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            return await dbSet
                .IncludeMultiple(includes)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        protected async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await dbSet.IncludeMultiple(includes).ToListAsync();
        }
    }

    public static class CommonExtension
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : BaseEntity
        {
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        public static string? GetMemberName<T>(Expression<Func<T, object>> expression)
            where T : BaseEntity
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                memberExpression = unaryExpression?.Operand as MemberExpression;
            }
            return memberExpression?.Member.Name;
        }
    }
}
