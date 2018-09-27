using JM.SCI.SalesPromo.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JM.SCI.SalesPromo.Model
{
    public class Campaign : BaseEntity
    {
        [Key]
        public int CampaignId { get; set; }
        [StringLength(256)]
        [Required]
        public string CampaignName { get; set; }
        public int? MaxNoOfWinner { get; set; }
        [StringLength(64)]
        public string PrimeCode { get; set; }
        [Required]
        public WinType WinType { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<CampaignWinner> CampaignWinners { get; set; }
    }
}
