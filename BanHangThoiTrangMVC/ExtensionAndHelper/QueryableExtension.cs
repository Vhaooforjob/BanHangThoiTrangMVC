using BanHangThoiTrangMVC.HelperModels.Paging;
using BanHangThoiTrangMVC.Strategy.SortingStrategy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BanHangThoiTrangMVC.ExtensionAndHelper
{
    public static class QueryableExtension
    {
        public static async Task<PagedResult<T>> ToPagingAsync<T, TEntity>(
          this IQueryable<TEntity> query,
          int page, int limit,
          Func<List<TEntity>, List<T>> mapper = null
          )
        {
            var result = new PagedResult<T>
            {
                Total = await query.CountAsync(),
                Page = page,
                Limit = limit
            };

            List<TEntity> items;
            if (limit > 0)
            {
                var startIndex = page * limit;
                items = await query
                    .Skip(startIndex < 0 ? 0 : startIndex)
                    .Take(limit)
                    .ToListAsync();
            }
            else
            {
                items = await query.ToListAsync();
            }
            if (mapper != null)
            {
                result.Items = mapper.Invoke(items ?? new List<TEntity>());
            }
            else
            {
                result.Items = (List<T>)(object)(items ?? new List<TEntity>());
            }

            return result;
        }

        public static IQueryable<T> OrderProperty<T>(
            this IQueryable<T> query,
            List<SortItems> sortItems,
            params (string key, string val)[] propertyMapping)
        {
            IOrderedQueryable<T> result = null;
            if (sortItems == null || !sortItems.Any()) return query;

            for (int i = 0; i < sortItems.Count; i++)
            {
                string propertyName = getProperty(sortItems[i].Column);
                ISortingStrategy<T> strategy;

                if (i == 0)
                {
                    strategy = sortItems[i].IsDesc
                        ? (ISortingStrategy<T>)new DescendingSortStrategy<T>()
                        : new AscendingSortStrategy<T>();
                    result = new SortingStrategyContext<T>(strategy).Sort(query, propertyName);
                }
                else
                {
                    strategy = sortItems[i].IsDesc
                        ? (ISortingStrategy<T>)new ThenByDescendingSortStrategy<T>()
                        : new ThenBySortStrategy<T>();
                    result = new SortingStrategyContext<T>(strategy).Sort(query, propertyName);
                }
            }

            return result ?? query;


            string getProperty(string property)
            {
                var key = property.ToPascalCase();
                var map = propertyMapping?.FirstOrDefault(x => x.key == key);
                return map.HasValue && map.Value.val != null ? map.Value.val : key;
            }
        }

        public static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                if (pi is null) continue;
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static string ToPascalCase(this string input)
        {
            return string.Join(".", input.Split('.').Select(x => ConvertToPascalCase(x)));
        }

        private static string ConvertToPascalCase(string input)
        {
            Regex invalidCharsRgx = new Regex(@"[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with undescore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(input, "_"), string.Empty)
                // split by underscores
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                // set first letter to uppercase
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }
    }
}

