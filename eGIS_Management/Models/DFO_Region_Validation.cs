using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGIS_Management.Models
{
    [Bind()]
    public class DFO_Region_Validation
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "DFO Region")]
        public string Region { get; set; }
    }
}