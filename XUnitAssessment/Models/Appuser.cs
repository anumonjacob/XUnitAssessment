using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XUnitAssessment.Models
{
    [Table("appuser_anu")]
    public class Appuser
    {
        [Key]
        [JsonIgnore]
        public int AppUserId { get; set; }

        public int? UserTypeId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public int? IsActive { get; set; }

        [ForeignKey("UserTypeId")]
        [JsonIgnore]
        public virtual Usertype? Usertype { get; set; }
    }
}
