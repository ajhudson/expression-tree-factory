using System;

namespace FunWithExpressionTrees
{
    public class Consumer : IConsumer
    {
        private readonly IRepository _repo;

        public Guid UniqueId { get; }

        public Consumer(IRepository repo)
        {
            this._repo = repo;
            this.UniqueId = Guid.NewGuid();
        }

        public Guid GetRepoId() => this._repo.RepositoryId;
    }
}
