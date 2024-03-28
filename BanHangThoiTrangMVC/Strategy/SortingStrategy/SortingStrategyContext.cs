using System.Linq;

namespace BanHangThoiTrangMVC.Strategy.SortingStrategy
{
    public class SortingStrategyContext<T>
    {
        private readonly ISortingStrategy<T> _strategy;

        public SortingStrategyContext(ISortingStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName)
        {
            return _strategy.Sort(source, propertyName);
        }
    }
}