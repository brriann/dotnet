﻿using System;
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
      public string Name { get; set; }
      [DisplayName("Skier #")]
      public int SkierNumber { get; set; }
   }
}
