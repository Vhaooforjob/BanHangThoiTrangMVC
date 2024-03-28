using BanHangThoiTrangMVC.ExtensionAndHelper;
using BanHangThoiTrangMVC.Strategy.SortingStrategy;
using System.Linq;

public class AscendingSortStrategy<T> : ISortingStrategy<T>
{
    public IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName)
    {
        return QueryableExtension.ApplyOrder<T>(source, propertyName, "OrderBy");
    }
}

public class DescendingSortStrategy<T> : ISortingStrategy<T>
{
    public IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName)
    {
        return QueryableExtension.ApplyOrder<T>(source, propertyName, "OrderByDescending");
    }
}

public class ThenBySortStrategy<T> : ISortingStrategy<T>
{
    public IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName)
    {
        return QueryableExtension.ApplyOrder<T>(source, propertyName, "ThenBy");
    }
}

public class ThenByDescendingSortStrategy<T> : ISortingStrategy<T>
{
    public IOrderedQueryable<T> Sort(IQueryable<T> source, string propertyName)
    {
        return QueryableExtension.ApplyOrder<T>(source, propertyName, "ThenByDescending");
    }
}