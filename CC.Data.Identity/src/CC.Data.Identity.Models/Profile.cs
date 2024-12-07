
using System.ComponentModel.DataAnnotations.Schema;
using CC.Data.Basics.Models;

namespace CC.Data.Identity.Models
{
	[Table("Profile")]
    public class Profile : BaseEntity
    {

        /// <summary>
        /// Navigation Properties
        /// </summary>
        public Guid IdentityId { get; set; }
        public Identity Identity { get; set; }


        public string Name { get; set; }
        public ProfileStatus Status { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? PreferredName { get; set; }
        public Title Title { get; set; }
        public Gender Gender { get; set; }
        public List<PronounChoice>? Pronouns { get; set; }
    }
}