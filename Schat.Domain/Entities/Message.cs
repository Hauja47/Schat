using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid ChannelId { get; set; }

        public string Content { get; set; }
    }
}
