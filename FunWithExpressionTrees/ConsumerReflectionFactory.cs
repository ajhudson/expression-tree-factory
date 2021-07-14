using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FunWithExpressionTrees
{
    public class ConsumerReflectionFactory : FactoryBase
    {
        public ConsumerReflectionFactory(IRepository repository) : base(repository)
        {
        }

        static ConsumerReflectionFactory()
        {
            FactoryDelegates = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(t => typeof(IConsumer).IsAssignableFrom(t) && t.IsClass)
                                .Select(t => new TypeInitInfo { Name = t.Name, CreateFn = repo => (IConsumer)Activator.CreateInstance(t, repo) })
                                .ToDictionary(ti => ti.Name, ti => ti.CreateFn);
        }
    }
}
