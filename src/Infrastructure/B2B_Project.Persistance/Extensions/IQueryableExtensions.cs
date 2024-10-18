using System.Linq.Expressions;

namespace B2B_Project.Persistance.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> query, string propertyName, object value, string operation)
        {
            if (value == null || string.IsNullOrEmpty(propertyName)) return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);

            // constant'ı Expression olarak tanımlayın
            Expression constant = Expression.Constant(value);

            // Eğer türler farklıysa, bir dönüşüm yapılır
            if (property.Type != constant.Type)
            {
                constant = Expression.Convert(constant, property.Type);  // Dönüşüm işlemi burada yapılır
            }

            Expression comparison = operation switch
            {
                ">" => Expression.GreaterThan(property, constant),
                "<" => Expression.LessThan(property, constant),
                "=" => Expression.Equal(property, constant),
                ">=" => Expression.GreaterThanOrEqual(property, constant),
                "<=" => Expression.LessThanOrEqual(property, constant),
                _ => throw new ArgumentException("Invalid comparison operation")
            };

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
            return query.Where(lambda);
        }
    }
}
