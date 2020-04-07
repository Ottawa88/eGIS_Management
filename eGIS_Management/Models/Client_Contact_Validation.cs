using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eGIS_Management.Models
{
    [Bind()]
    public class Client_Contact_Validation
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

     
        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
    }
}