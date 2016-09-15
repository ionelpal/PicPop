using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicPop.Models
{
    public class HomeViewModel
    {
        [Required]
        public int? CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        public int NumImages { get; set; }

        public List<string> ListImages { get; set; }
        public List<string> LastImagesInserted { get; set; }
    }

}