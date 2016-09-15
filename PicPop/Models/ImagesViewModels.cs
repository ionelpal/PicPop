using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PicPop.BusinessLogic;
using PicPop.Helper;

namespace PicPop.Models
{
    
    public class ImagesCreateViewModels
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Image")]
        [ValidateFile]
        public HttpPostedFileBase File { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

    public class ImagesEditViewModels
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Blob { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }

    public class ImagesDetailsViewModels
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Blob { get; set; }

        public string Category { get; set; }

        public bool IsMine { get; set; }
    }

    public class ImageSearchViewModel
    {
        public IPagedList<ImagesDetailsViewModels> List { get; set; }

        public int? Id { get; set; }

        [Display(Name = "Select by Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }


    public class ImagesListViewModels
    {
        public List<PicPopImage> List { get; set; }
    }


    public class ImagesShop
    {
        public List<ImagesDetailsViewModels> List { get; set; }
    }
}