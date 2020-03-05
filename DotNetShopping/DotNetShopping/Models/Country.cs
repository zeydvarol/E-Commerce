using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetShopping.Models
{
    public class Country
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CountryId { get; set; }
        [Required]
        public string code { get; set; }

        [Required]//bu alanı boş bırakamazsın
        [Display(Name = "Country Name")]
        public string Name { get; set; }
    }
}