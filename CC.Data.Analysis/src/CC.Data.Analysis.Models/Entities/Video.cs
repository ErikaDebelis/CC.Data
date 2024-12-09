using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using CC.Data.Chat.Models;
using CC.Data.Basics.Models;
using CC.Data.Identity.Models;

namespace CC.Data.Analysis.Models.Entities
{
    [PrimaryKey(nameof(Id))]
    [Table("Video")]
    public class Video: BaseEntity
    {
        /// <summary>
        /// Navigation property
        /// </summary>
        public Guid IdentityId { get; set; }
        public Identity.Models.Identity Identity { get; set; }

        public Guid ChatId { get; set; }
        public Chat.Models.Entities.Chat Chat { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public VideoType VideoType { get; set; }
        public TimeSpan Duration { get; set; }
        public List<EnhancedCaption> Captions { get; set; }
    }
}