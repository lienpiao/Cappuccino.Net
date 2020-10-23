using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Cappuccino.IDAL
{
    /// <summary>
    /// 数据访问接口基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseDao<T> where T : class, new()
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="whereLambad"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="isASC"></param>
        /// <returns></returns>
        IQueryable<T> GetListByPage<S>(
            Expression<Func<T, bool>> whereLambad,
            Expression<Func<T, S>> orderBy,
            int pageSize,
            int pageIndex,
            out int totalCount,
            bool isASC);

        /// <summary>
        /// 查询总数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int GetRecordCount(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddList(params T[] entities);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int DeleteBy(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertys">要修改的字段</param>
        T Update(T entity, string[] propertys);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int UpdateList(params T[] entities);

        int SaveChanges();

        /// <summary>
        /// 对数据库执行给定的 DDL/DML 命令。
        /// </summary>
        /// <param name="sql">命令字符串。</param>
        /// <param name="parameters">要应用于命令字符串的参数。</param>
        /// <returns> 执行命令后由数据库返回的结果。</returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回给定泛型类型的元素。
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="sql">  SQL 查询字符串。</param>
        /// <param name="parameters"> 要应用于 SQL 查询字符串的参数。</param>
        /// <returns>查询所返回对象的类型</returns>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
    }
}
