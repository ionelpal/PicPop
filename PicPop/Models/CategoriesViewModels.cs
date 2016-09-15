using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PicPop.Models
{
    public class CategoriesViewModels
    {
    }


    public class CategoriesCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }


    // Edit/Details
    public class CategoriesEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class CategoriesIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }

    }
}