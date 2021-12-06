using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace eGIS_Management.Models
{
    [Bind()]
    public class GIS_Server_Validation
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "Server Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Server FQDN")]
        public string FQDN { get; set; }

        [Required]
        [Display(Name = "Network Zone")]
        public short Zone_ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Port in Use")]
        public string Port_in_Use { get; set; }

        [Required]
        [RegularExpression(@"^([\d]{1,3}\.){3}[\d]{1,3}$")]
         
        [StringLength(50)]
        [Display(Name = "Server IP Address")]
        public string IP_Address { get; set; }

        [Required]
        [Display(Name = "DFO Region")]
        public Nullable<short> Region_ID { get; set; }

        [Required]
        [Display(Name = "GIS Environment")]
        public int gis_environment_ID { get; set; }

        [Required]
        [Display(Name = "MS Windows")]
        public short OS_ID { get; set; }

        [Required]
        [Display(Name = "Diskspace (GB)")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Diskspace_GB { get; set; }

        [Required]
        [Display(Name = "RAM (GB)")]
        [Range(0, Int16.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public short RAM_GB { get; set; }

        [Required]
        [Display(Name = "No. of Virtual Processors")]
        [Range(0, Int16.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public short CPU { get; set; }

        [StringLength(50)]
        [Display(Name = "SSL Certificate Issued to")]
        public string SSL_IssuedTo { get; set; }

        [StringLength(50)]
        [Display(Name = "SSL Certificate Issued by")]
        public string SSL_IssuedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SSL_ExpiryDate { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }
    }
}