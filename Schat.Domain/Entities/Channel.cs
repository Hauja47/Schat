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

        public required string Name { get; set; }

        public required IEnumerable<UserInfo> Members { get; set; }
    }
}
