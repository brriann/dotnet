using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mvc1.Models
{
   public class Skier
   {
      [Key]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
      [DisplayName("Skier #")]
      [Required]
      [Range(1,int.MaxValue,ErrorMessage ="Skier Number must be greater than 0")]
      public int SkierNumber { get; set; }
      public bool IsSlalom { get; set; }
      public bool IsTrick { get; set; }
      [DisplayName("Salary Cap")]
      public int SalaryCap { get; set; }
      [DisplayName("Betting Line")]
      public double BettingLine { get; set; }
   }
}
