using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Sum(s => s.Total) > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(c =>
                (customer: c, suppliers: suppliers.Where(s => c.Country == s.Country && c.City == s.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.GroupJoin(suppliers, s => s.Country, n => n.Country,
                (cus, sup) => (customer: cus, suppliers: sup.Where(s => s.City == cus.City)));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders != null && c.Orders.Length != 0).Select(c =>
                (customer: c, dateOfEntry: c.Orders.OrderBy(o => o.OrderDate).First().OrderDate));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            return Linq4(customers).OrderBy(s => s.dateOfEntry.Year).ThenBy(s => s.dateOfEntry.Month)
                .ThenByDescending(c => c.customer.Orders.Sum(a => a.Total)).ThenBy(s => s.customer.CustomerID);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return customers.Where(c =>
                int.TryParse(c.PostalCode, out var e) == false || string.IsNullOrEmpty(c.Region) ||
                !Regex.IsMatch(c.Phone, "^\\(\\d+\\)"));
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            return products.GroupBy(p => p.Category).Select(s => new Linq7CategoryGroup
            {
                Category = s.Key,
                UnitsInStockGroup = s.GroupBy(v => v.UnitsInStock).Select(a => new Linq7UnitsInStockGroup
                { UnitsInStock = a.Key, Prices = a.Select(j => j.UnitPrice).OrderBy(d => d) })
            });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var listEnum = products.ToList();
            return new[]
            {
                (category: cheap, products: listEnum.Where(p => p.UnitPrice <= cheap)),
                    (category: middle, products: listEnum.Where(p => p.UnitPrice > cheap && p.UnitPrice <= middle)),
                        (category: expensive,
                            products: listEnum.Where(p => p.UnitPrice > middle && p.UnitPrice <= expensive))
            };
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            return customers.Where(c => c.Orders != null).GroupBy(c => c.City).Select(s => (city: s.Key,
                avarageIncome: (int)Math.Round(s.SelectMany(a => a.Orders).Sum(d => d.Total) / s.Count()),
                averageIntenity: (int)s.Average(v => v.Orders.Length)));
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            return string.Concat(suppliers.OrderBy(s => s.Country.Length).ThenBy(s => s.Country).Select(s => s.Country)
                .Distinct());
        }
    }
}