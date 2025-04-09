using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class Channel: BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<User> Members { get; set; }
    }
}
