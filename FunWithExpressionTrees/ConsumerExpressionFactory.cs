using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FunWithExpressionTrees
{
    public class ConsumerExpressionFactory : FactoryBase
    {

        static ConsumerExpressionFactory()
        {
            FactoryDelegates = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(t => typeof(IConsumer).IsAssignableFrom(t) && t.IsClass)
                                .Select(t =>
                                {
                                    string typeName = t.Name;
                                    var constructorInfo = t.GetConstructor(new Type[] { typeof(IRepository) });
                                    ParameterExpression paramExpr = Expression.Parameter(typeof(IRepository));
                                    NewExpression newExpr = Expression.New(constructorInfo, paramExpr);
                                    UnaryExpression typeConvertExpr = Expression.Convert(newExpr, typeof(IConsumer));
                                    var factoryFn = Expression.Lambda<Func<IRepository, IConsumer>>(typeConvertExpr, paramExpr).Compile();

                                    return new TypeInitInfo { Name = t.Name, CreateFn = factoryFn };
                                })
                                .ToDictionary(ti => ti.Name, ti => ti.CreateFn );
        }

        public ConsumerExpressionFactory(IRepository repository) : base(repository)
        {
        }
    }
}
