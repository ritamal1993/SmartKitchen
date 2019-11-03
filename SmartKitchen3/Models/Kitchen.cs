using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartKitchen3.Models
{
    public class Kitchen
    {
        public int KitchenId { get; set; }

        [Display(Name = "Kitchen Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kitchen name is required")]
        public string Name { get; set; }
        public double? LocationLatitudes { get; set; }
        public double? LocationLongitude { get; set; }
    }
}