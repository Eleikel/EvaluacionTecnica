using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Persistence.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long IdCard { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string TransactionUser { get; set; }
        public DateTime TransactionDate { get; set; }
        public virtual Role? Role { get; set; }

    }
}
