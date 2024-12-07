using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

using CC.Data.Basics.Models;
using CC.Data.Identity.Models;

namespace CC.Data.Chat.Models.Entities
{
    [PrimaryKey(nameof(Id))]
    [Table("Chat")]
    public class Chat: BaseEntity
    {
        public string Name { get; set; }
        public ChatStatus ChatStatus { get; set; }
        public List<Message> Messages { get; set; }
    }
}