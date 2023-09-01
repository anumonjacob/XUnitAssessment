using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XUnitAssessment.Models
{
    [Table("usertype_anu")]
    public class Usertype
    {
        [Key]
        [JsonIgnore]
        public int? UserTypeId { get; set; }
        
        public string? UserType { get; set; }
        
        public string? Description { get; set;}

        public int? IsActive { get; set;}
    }
}
