using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TransactionUser { get; set; }
        public DateTime TransactionDate { get; set; }
        public virtual ICollection<User?> Users { get; set; }

    }
}
