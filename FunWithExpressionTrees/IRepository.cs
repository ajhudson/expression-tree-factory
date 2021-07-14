using System;
using System.Threading.Tasks;

namespace FunWithExpressionTrees
{
    public interface IRepository
    {
        Task<int> CountSomeThingsAsync();

        Guid RepositoryId { get;  }
    }
}
