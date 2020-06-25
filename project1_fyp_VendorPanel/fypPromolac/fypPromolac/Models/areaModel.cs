using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class areaModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public areaModel()
        {
            this.notifications = new HashSet<notification>();
            this.areaAssigneds = new HashSet<areaAssigned>();
        }

        public int areaId { get; set; }
        public string areaName { get; set; }
        public string areaHashCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification> notifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<areaAssigned> areaAssigneds { get; set; }
    }
}