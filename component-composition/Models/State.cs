﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace component_composition.Models
{
    [Table("State")]
    public class State
    {
        [Key]
        public int StateID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
