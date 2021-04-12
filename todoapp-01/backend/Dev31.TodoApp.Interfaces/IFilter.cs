namespace Dev31.TodoApp.Interfaces
{
    using System.Linq;

    public interface IFilter<T>
    {
        public IQueryable<T> Filter(IQueryable<T> items);
    }
}
