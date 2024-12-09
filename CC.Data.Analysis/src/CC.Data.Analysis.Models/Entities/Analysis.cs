﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

using CC.Data.Basics.Models;
using CC.Data.Identity.Models;

namespace CC.Data.Analysis.Models.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Analysis: BaseEntity
    {
        /// <summary>
        /// Navigation property
        /// </summary>
        public Guid CaptionId { get; set; }
        public EnhancedCaption Caption { get; set; }

        public Guid VideoId { get; set; }
        public Video Video { get; set; }

        public string EnhancedDescription { get; set; }
        public string EmotionalContext { get; set; }
        public string? NonVerbalCues { get; set; }
        public Dictionary<string, string>? Metadata { get; set; }
    }
}