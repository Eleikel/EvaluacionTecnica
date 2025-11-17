using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Business.Interfaces.Common
{
    public interface IEditable<T, TKey> where T : class
    {
        public Task<T> Create(T entity);
        public Task<T> Update(TKey id, T entity);
        public Task<bool> Delete(TKey id);
    }
}
