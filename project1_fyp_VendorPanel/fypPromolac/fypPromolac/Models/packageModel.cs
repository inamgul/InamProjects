using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class packageModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public packageModel()
        {
            this.userPackages = new HashSet<userPackageModel>();
        }
        [Key]
        public int packagesId { get; set; }
        public string packageName { get; set; }
        public int messagesAllowed { get; set; }
        public int subUsersAllowed { get; set; }
        public int packageDurationDays { get; set; }
        public string packageDescription { get; set; }
        public int fencingHours { get; set; }

        public DateTime endtime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userPackageModel> userPackages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        

       
        [NotMapped]
        [DisplayName("Areas Allocated:")]
        public List<areaAssignedModel> detail { get; set; }
    }
}