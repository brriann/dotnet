using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mvc1.Models
{
   public class Skier
   {
      [Key]
      public int Id { get; set; }
      [Required]
      [Column(TypeName ="nvarchar(20)")]
      public string Name { get; set; }
      [DisplayName("Skier #")]
      [Required]
      [Range(1,int.MaxValue,ErrorMessage ="Skier Number must be greater than 0")]
      public int SkierNumber { get; set; }
      [Required]
      public bool IsSlalom { get; set; }
      [Required]
      public bool IsTrick { get; set; }
      [Column("IsJ")]
      [Required]
      public bool IsJump { get; set; }
      [Required]
      [DisplayName("Salary Cap")]
      public int SalaryCap { get; set; }
      [DisplayName("Betting Line")]
      public double BettingLine { get; set; }
      [NotMapped]
      public DateTime LoadedFromDatabase { get; set; }
      
   }
}
