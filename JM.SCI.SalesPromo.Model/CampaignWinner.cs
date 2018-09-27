using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JM.SCI.SalesPromo.Model
{
    public class CampaignWinner : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public int CampaignId { get; set; }
        public  Campaign Campaign { get; set; }
        [StringLength(128)]
        [Key]
        [Column(Order = 2)]
        public string CouponCode { get; set; }
        public int WinnerId { get; set; }
        public virtual Winner Winner { get; set; }
    }
}
