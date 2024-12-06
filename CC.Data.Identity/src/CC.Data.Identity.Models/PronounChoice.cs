
using System.ComponentModel.DataAnnotations.Schema;
using CC.Data.Basics.Models;

namespace CC.Data.Identity.Models
{
	[Table("PronounChoice")]
    public class PronounChoice : BaseEntity
    {
        public Pronoun Pronoun { get; set; }
    }
}