using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schat.Domain.Entities
{
    public class BaseEntity
    {
        public Instant CreatedDate { get; set; }

        public Instant? UpdatedDate { get; set; }
    }
}
