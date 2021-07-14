using System;
using System.Threading.Tasks;

namespace FunWithExpressionTrees
{
    public class ConcreteRepository : IRepository
    {
        private readonly Guid _guid;

        public Guid RepositoryId => this._guid;

        public ConcreteRepository()
        {
            this._guid = Guid.NewGuid();
        }

        public async Task<int> CountSomeThingsAsync()
        {
            return await Task.FromResult(101);
        }
    }
}
