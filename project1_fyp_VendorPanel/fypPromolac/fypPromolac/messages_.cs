//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fypPromolac
{
    using System;
    using System.Collections.Generic;
    
    public partial class messages_
    {
        public Nullable<int> vendorID { get; set; }
        public Nullable<int> adminID { get; set; }
        public string messageDescription { get; set; }
        public int areaId { get; set; }
        public string messageStatus { get; set; }
        public string messageTitle { get; set; }
        public int messageId { get; set; }
    
        public virtual area area { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
        public virtual vendor vendor { get; set; }
    }
}