using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new() // T parametresi alan, Entity ise, class ise 
    {
        T Get(Expression<Func<T, bool>> filter); // tek nesne getirme
        IList<T> GetList(Expression<Func<T, bool>> filter = null); // nesne listeleme
        T Add(T entity); // Ekleme
        T Update(T entity); // Güncelleme
        void Delete(T entity); // tekli silme
        void DeleteRange(List<T> entity); // toplu silme
    }
}
