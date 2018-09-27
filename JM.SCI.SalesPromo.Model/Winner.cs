using System.ComponentModel.DataAnnotations;

namespace JM.SCI.SalesPromo.Model
{
    public class Winner : BaseEntity
    {
        [Key]
        public int WinnerId { get; set; }
        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(128)]
        [Required]
        public string LastName { get; set; }
        [StringLength(256)]
        [Required]
        public string AddressLine { get; set; }
        [StringLength(32)]
        [Required]
        public string PostalCode { get; set; }
    }
}
