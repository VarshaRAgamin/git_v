using System;
using System.Collections.Generic;
using System.Text;

namespace Vroom_Project.Entities
{
    public class Sample : BaseEntity,IBaseEntity
    {
        public string Description { get; set; }
        public int TenantId { get; set; }
    }
}
