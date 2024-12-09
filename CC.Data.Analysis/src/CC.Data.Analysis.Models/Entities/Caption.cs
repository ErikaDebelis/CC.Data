using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using CC.Data.Basics.Models;
using CC.Data.Identity.Models;

namespace CC.Data.Analysis.Models.Entities
{
    [PrimaryKey(nameof(Id))]
    public class EnhancedCaption: BaseEntity
    {
        /// <summary>
        /// Navigation property
        /// </summary>
        public Guid VideoId { get; set; }
        public Video Video { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string StandardText { get; set; }
        public List<Analysis> Analyses { get; set; }
        public string? BackgroundContext { get; set; }
        public string? SoundEffects { get; set; }
    }
}