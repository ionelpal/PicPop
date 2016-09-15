using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PicPop.Helper
{
    //Customized data annotation validator for uploading file
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var allowedFileExtensions = new[] { ".jpg", ".png" };

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;

            if (allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                return true;

            ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", allowedFileExtensions);
            return false;
        }
    }
}