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
      public string Name { get; set; }
      [DisplayName("Tourney #")]
      public int TourneyNumber { get; set; }
   }
}
