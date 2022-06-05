using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShinCacheTensei.Data.Models
{
    public class DemonQueryParams
    {
        [Required]
        [MinLength(1, ErrorMessage = "Number of ids cannot be below 1.")]
        [MaxLength(5, ErrorMessage = "Number of ids cannot exceed 5.")]
        [FromQuery(Name = "id")]
        public int[] Ids { get; set; }
    }
}