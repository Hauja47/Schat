using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace Schat.Domain.Entities;

using Enum;

public abstract class BaseEntity
{
    public Instant CreatedDate { get; set; }

    public Instant? UpdatedDate { get; set; }
        
    public RecordStatus RecordStatus { get; set; }
}