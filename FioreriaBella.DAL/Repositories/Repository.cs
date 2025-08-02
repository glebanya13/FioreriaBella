using System.Linq.Expressions;
using FioreriaBella.DAL.Interfaces;
using FioreriaBella.DAL;
using Microsoft.EntityFrameworkCore;

namespace FioreriaBella.DAL.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
  {
    protected readonly ApplicationContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }

    public virtual IEnumerable<T> GetAll()
    {
      return _dbSet.ToList();
    }

    public virtual T GetById(int id)
    {
      return _dbSet.Find(id);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
      return _dbSet.Where(predicate);
    }

    public virtual IQueryable<T> FindQuery(Expression<Func<T, bool>> predicate)
    {
      return _dbSet.Where(predicate);
    }

    public virtual void Add(T entity)
    {
      _dbSet.Add(entity);
    }

    public virtual void Update(T entity)
    {
      _dbSet.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Remove(T entity)
    {
      _dbSet.Remove(entity);
    }

    public virtual void SaveChanges()
    {
      _context.SaveChanges();
    }
  }
}
