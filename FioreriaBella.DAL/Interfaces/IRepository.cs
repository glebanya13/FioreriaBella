using System.Linq.Expressions;

namespace FioreriaBella.DAL.Interfaces
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll();
    T GetById(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

    IQueryable<T> FindQuery(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    void SaveChanges();
  }
}
