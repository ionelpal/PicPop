//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PicPop.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    
    public partial class Profile
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Telephone { get; set; }
        public int GenderId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
