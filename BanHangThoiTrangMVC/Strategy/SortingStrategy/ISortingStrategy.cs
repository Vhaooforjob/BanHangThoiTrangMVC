using System.Linq;

namespace BanHangThoiTrangMVC.Strategy.SortingStrategy
{
    public interface ISortingStrategy<T>
    {
        IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName);
    }
}
