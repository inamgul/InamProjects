using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public partial class pkg
    {
        [DisplayName("Package Id:")]
        public int pkgId { get; set; }
        [DisplayName("Package Name:")]

        public string pakgName { get; set; }
        [DisplayName("Sub Users Allowed:")]
        public int subUserAllowed { get; set; }
        [DisplayName("Number of Notification Allowed:")]
        public int notfAllowed { get; set; }


        [NotMapped]
        [DisplayName("Areas Allocated:")]
        public List<area> detail { get; set; }
    }
}