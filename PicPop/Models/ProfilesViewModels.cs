using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PicPop.BusinessLogic;

namespace PicPop.Models
{
    public class ProfilesEditViewModels
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime? DoB { get; set; }

        public string Telephone { get; set; }

        [Required]
        public GenderType Gender { get; set; }
    }

    public class ProfilesDetailsViewModels
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime? DoB { get; set; }

        public string Telephone { get; set; }

        public GenderType Gender { get; set; }
    }
}