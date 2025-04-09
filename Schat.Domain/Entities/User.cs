using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<Channel>? JoinedChannels { get; set; }
    }
}
