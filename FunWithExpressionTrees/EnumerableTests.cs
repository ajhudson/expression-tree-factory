using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunWithExpressionTrees
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }
    }

    [TestFixture]
    public class EnumerableTests
    {
        [Test]
        public void ShouldFilterByExpressionTree()
        {
            // Arrange
            var p1 = new Person { FirstName = "Jamie", LastName = "Zachery", Id = 1 };
            var p2 = new Person { FirstName = "Scott", LastName = "Dawber", Id = 2 };
            var p3 = new Person { FirstName = "Mary", LastName = "Smith", Id = 3 };
            var people = new List<Person>(new[] { p1, p2, p3 } );

            var linqResult = people.Where(p => p.LastName == "Smith").FirstOrDefault();

            // Act
            var propInfo = typeof(Person).GetProperty("LastName");

            if (propInfo == null)
            {
                throw new NullReferenceException(nameof(propInfo));
            }

            var collectionOf = typeof(Person);
            var paramExpr = Expression.Parameter(collectionOf, "x");
            var propAccess = Expression.MakeMemberAccess(paramExpr, propInfo);
            var criteria = Expression.Constant("Smith");
            var expr = Expression.Equal(propAccess, criteria);
            var whereFn = Expression.Lambda<Func<Person, bool>>(expr, paramExpr).Compile();
            var ret = people.Where(whereFn);


            

            // Assert
        }
    }
}
