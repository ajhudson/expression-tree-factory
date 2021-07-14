using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithExpressionTrees
{
    public abstract class FactoryBase : IConsumerFactory
    {
        protected readonly IRepository _repo;

        protected static Dictionary<string, Func<IRepository, IConsumer>> FactoryDelegates;

        protected FactoryBase(IRepository repository)
        {
            this._repo = repository;
        }

       

        protected class TypeInitInfo
        {
            public string Name { get; set; }

            public Func<IRepository, IConsumer> CreateFn { get; set; }
        }

        public IConsumer CreateInstance(string typeName)
        {
            if (!FactoryDelegates.ContainsKey(typeName))
            {
                throw new Exception($"{typeName} does not exist");
            }

            var factoryFn = FactoryDelegates[typeName];

            return factoryFn(this._repo);
        }
    }
}
