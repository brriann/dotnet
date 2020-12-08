using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc1.Models
{
   public class Tourney
   {
      [Key]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
      [Required]
      [DisplayName("Tourney #")]
      [Range(1, int.MaxValue, ErrorMessage = "Tourney Number must be greater than 0")]
      public int TourneyNumber { get; set; }
   }
}
