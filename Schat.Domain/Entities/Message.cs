using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class Message : BaseEntity
    {
        public Guid UserInfoId { get; set; }
        
        public required UserInfo UserInfo { get; set; }

        public Guid ChannelId { get; set; }
        
        public required Channel Channel { get; set; }

        public required string Content { get; set; }
    }
}
