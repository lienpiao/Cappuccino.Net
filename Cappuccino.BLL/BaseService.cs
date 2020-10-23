using Cappuccino.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cappuccino.BLL
{
    public abstract class BaseService<T> : IDisposable where T : class, new()
    {

        protected IBaseDao<T> CurrentDao;

        public BaseService()
        {
            this.DisposableObjects = new List<IDisposable>();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDao.GetList(whereLambda);
        }

        public IQueryable<T> GetListByPage<S>(
            Expression<Func<T, bool>> whereLambada,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC)
        {
            return this.CurrentDao.GetListByPage<S>(
                whereLambada,
                orderBy,
                pageSize,
                pageIndex,
                out totalCount,
                isASC);
        }

        public int GetRecordCount(Expression<Func<T, bool>> predicate)
        {
            return this.CurrentDao.GetRecordCount(predicate);
        }

        public int Add(T entity)
        {
            this.CurrentDao.Add(entity);
            return CurrentDao.SaveChanges();
        }

        public int AddList(params T[] entities)
        {
            return this.CurrentDao.AddList(entities);
        }

        public int Delete(T entity)
        {
            this.CurrentDao.Delete(entity);
            return CurrentDao.SaveChanges();
        }

        public int DeleteBy(Expression<Func<T, bool>> whereLambda)
        {
            this.CurrentDao.DeleteBy(whereLambda);
            return CurrentDao.SaveChanges();
        }

        public bool Update(T entity)
        {
            this.CurrentDao.Update(entity);
            return this.CurrentDao.SaveChanges() > 0;
        }

        public virtual bool Update(T entity, string[] propertys)
        {
            this.CurrentDao.Update(entity, propertys);
            return this.CurrentDao.SaveChanges() > 0;
        }

        public int UpdateList(params T[] entities)
        {
            return this.CurrentDao.UpdateList(entities);
        }

        public IList<IDisposable> DisposableObjects { get; private set; }

        protected void AddDisposableObject(object obj)
        {
            IDisposable disposable = obj as IDisposable;
            if (disposable != null)
            {
                this.DisposableObjects.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (IDisposable obj in this.DisposableObjects)
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
        }
    }
}
