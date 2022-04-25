using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Entities;

namespace Infrastructure.Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static IQueryable<Product> sort
        (this IQueryable<Product> products,
        string orderByOptions)
        {
            switch (orderByOptions)
            {
                case "name":
                    return products.OrderBy(
                    p=> p.Name);
                case "namedesc":
                    return products.OrderByDescending(
                    p=> p.Name);
               
                default:
                     return products.OrderByDescending(
                    p=> p.Id);
            }
        }
    }
}