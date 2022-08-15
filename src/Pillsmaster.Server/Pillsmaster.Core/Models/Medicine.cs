﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pillsmaster.Domain.Models
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }

        [StringLength(30)]
        [Required]
        public string TradeName { get; set; }

        [StringLength(30)]
        public string InternationalName { get; set; } = string.Empty;

        [StringLength(30)]
        public string PharmaType { get; set; } = string.Empty;

        public int ActiveIngredientCount { get; set; }

        [JsonIgnore]
        public ICollection<UserMedicine>? UserMedicines { get; set; }
    }
}