using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.Interfaces.Common
{
    public interface IRead<T, TKey> where T : class
    {
        public Task<IEnumerable<T>> Get();
        public Task<T> GetById(TKey id);
    }
}
