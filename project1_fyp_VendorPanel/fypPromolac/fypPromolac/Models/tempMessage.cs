using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class tempMessage
    {
        public string notificationId { get; set; }
        public Nullable<int> vendorID { get; set; }
        public Nullable<int> adminID { get; set; }
        public string notificationDescription { get; set; }
        public string notificationAreaId { get; set; }
        public string notificationStatus { get; set; }
        public string notificationTitle { get; set; }
        [NotMapped]
        public List<areaAssignedModel> detail { get; set; }
        [NotMapped]
        public List<string> areaName { get; set; }
        public virtual area area { get; set; }
        public string notificationTime { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
        public virtual vendor vendor { get; set; }
    }
}