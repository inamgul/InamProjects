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
    
    public partial class package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public package()
        {
            this.userPackages = new HashSet<userPackage>();
        }
    
        public int packagesId { get; set; }
        public string packageName { get; set; }
        public int messagesAllowed { get; set; }
        public int subUsersAllowed { get; set; }
        public int packageDurationDays { get; set; }
        public string packageDescription { get; set; }
        public int fencingHours { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userPackage> userPackages { get; set; }
    }
}
