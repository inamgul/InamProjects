using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class dealCategoryModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dealCategoryModel()
        {
            this.deals = new HashSet<deal>();
        }

        public int categoryId { get; set; }
        public string categoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<deal> deals { get; set; }
    }
}