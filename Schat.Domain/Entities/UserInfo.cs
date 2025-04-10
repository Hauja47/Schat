using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class UserInfo : BaseEntity
    {
        public Guid Id { get; set; }

        public required string IdentityId { get; set; }
        
        public required string FullName { get; set; }

        public IEnumerable<Channel>? JoinedChannels { get; set; }
    }
}
