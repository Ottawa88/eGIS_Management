using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace eGIS_Management.Models
{
    [Bind()]
    public class Application_Validation
    {
        [Required]
        [StringLength(500)]
        [Display(Name = "Application Name")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public Nullable<int> APM_ID { get; set; }

        [Required]
        [Display(Name = "GIS Environment")]
        public int GIS_Environment_ID { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Application URL (En)")]
        public string URL_En { get; set; }

      
        [StringLength(500)]
        [Display(Name = "Application URL (Fr)")]
        public string URL_Fr { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Main Tech Support")]
        public int TechSupport1 { get; set; }

      
        [Display(Name = "2nd Tech Support")]
        public Nullable<int> TechSupport2 { get; set; }

        [Display(Name = "Main Client Contact")]
        public Nullable<int> ClientContact1 { get; set; }

        [Display(Name = "2nd Client Contact")]
        public Nullable<int> ClientContact2 { get; set; }

        [StringLength(500)]
        [Display(Name = "DevOps link (En)")]
        public string DevOps_Link_En { get; set; }


        [StringLength(500)]
        [Display(Name = "Technical Document Link")]
        public string Technical_Doc_Link { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Application Type")]
        public int TypeID{ get; set; }

        [Required]
        [Display(Name = "DFO  Region")]
        public int RegionID { get; set; }

        [Required]
        [Display(Name = "Region Sector")]
        public int SectorID { get; set; }

    }
}