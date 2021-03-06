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
    
    public partial class PicPopImage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PicPopImage()
        {
            this.BlobFiles = new HashSet<BlobFile>();
            this.TransactionItems = new HashSet<TransactionItem>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public System.DateTime DtAdded { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DtModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Active { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlobFile> BlobFiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
