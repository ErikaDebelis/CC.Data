
using System.ComponentModel.DataAnnotations.Schema;
using CC.Data.Basics.Models;

namespace CC.Data.Identity.Models
{
    /// <summary>
    /// join table for Profile and PronounChoice
    /// </summary>
    public class ProfilePronounChoice
    {
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Guid PronounChoiceId { get; set; }
        public PronounChoice PronounChoice { get; set; }
    }
}