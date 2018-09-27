using System;

namespace JM.SCI.SalesPromo.Model
{
    public class BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Deleted { get; set; }
    }
}
