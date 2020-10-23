using Cappuccino.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Cappuccino.DAL
{
    public class BaseDao<T> : IDisposable where T : class, new()
    {
        private DbContext Db
        {
            get { return DbContextFactory.GetCurrentThreadInstance(); }
        }

        private IDbSet<T> dbSet;

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> DbSet
        {
            get
            {
                this.dbSet = this.dbSet ?? Db.Set<T>();
                return this.dbSet;
            }
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            IQueryable<T> result = DbSet.Where(whereLambda);
            return result;
        }

        public virtual IQueryable<T> GetListByPage<S>(
            Expression<Func<T, bool>> whereLambada,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC)
        {
            totalCount = DbSet.Where(whereLambada).Count();
            IQueryable<T> entities = null;
            if (isASC)
            {
                entities = DbSet.Where(whereLambada)
                    .OrderBy(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
            }
            else
            {
                entities = DbSet.Where(whereLambada)
                    .OrderByDescending(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize);
            }
            return entities;
        }

        public virtual int GetRecordCount(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).Count();
        }

        public virtual T Add(T entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public virtual int AddList(params T[] entities)
        {
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] == null) continue;
                DbSet.Add(entities[i]);
                //每累计到10条记录就提交
                if (i != 0 && i % 10 == 0)
                {
                    result += Db.SaveChanges();
                }
            }

            //可能还有不到10条的记录
            if (entities.Count() > 0)
            {
                result += Db.SaveChanges();
            }
            return result;
        }

        public virtual int Delete(T entity)
        {
            DbSet.Attach(entity);
            Db.Entry(entity).State = EntityState.Deleted;
            return -1;
        }

        public virtual int DeleteBy(Expression<Func<T, bool>> whereLambda)
        {
            var entitiesToDelete = DbSet.Where(whereLambda);
            foreach (var item in entitiesToDelete)
            {
                Db.Entry(item).State = EntityState.Deleted;
            }
            return -1;
        }

        public virtual T Update(T entity)
        {
            if (entity != null)
            {
                DbSet.Attach(entity);
                Db.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }

        public virtual T Update(T entity, string[] propertys)
        {
            if (entity != null)
            {
                if (propertys.Any() != false)
                {
                    //将model追击到EF容器
                    Db.Entry(entity).State = EntityState.Unchanged;
                    foreach (var item in propertys)
                    {
                        Db.Entry(entity).Property(item).IsModified = true;
                    }

                    //关闭EF对于实体的合法性验证参数
                    Db.Configuration.ValidateOnSaveEnabled = false;
                }
            }

            return entity;
        }

        public virtual int UpdateList(params T[] entities)
        {
            int result = 0;
            for (int i = 0; i < entities.Count(); i++)
            {
                if (entities[i] == null) continue;
                DbSet.Attach(entities[i]);
                Db.Entry(entities[i]).State = EntityState.Modified;
                if (i != 0 && i % 10 == 0)
                {
                    result += Db.SaveChanges();
                }
            }

            //可能还存在不到10条的记录
            if (entities.Count() > 0)
            {
                result += Db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 释放EF上下文
        /// DbContext有默认的垃圾回收机制，但通过BaseRepository实现IDisposable接口，可以在不用EF上下文的时候手动回收，时效性更强。
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Db.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return Db.Database.SqlQuery<TElement>(sql, parameters);
        }
    }
}
