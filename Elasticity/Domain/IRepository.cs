using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Domain
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate);
        T GetById(Guid id);
    }
}
