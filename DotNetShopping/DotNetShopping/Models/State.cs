using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetShopping.Models
{
    public class State
    {
        [HiddenInput(DisplayValue = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 StateId { get; set; }
        [Required]
        [Display(Name = "State Name")]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public Int64 CountryId { get; set; }
        public Country Country { get; set; }
    }
}