using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using CC.Data.Basics.Models;
using CC.Data.Identity.Models;

namespace CC.Data.Chat.Models.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Message: BaseEntity
    {
        /// <summary>
        /// Navigation property
        /// </summary>
        public Guid ChatId { get; set; }
        public Chat? Chat { get; set; }

        public Guid ToIdentityId { get; set; }
        public Identity.Models.Identity? ToIdentity { get; set; }

        public Guid FromIdentityId { get; set; }
        public Identity.Models.Identity? FromIdentity { get; set; }

        /// <summary>
        /// The text of the message
        /// </summary>
        public string Text { get; set; }

        public MessageType MessageType { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
    }
}