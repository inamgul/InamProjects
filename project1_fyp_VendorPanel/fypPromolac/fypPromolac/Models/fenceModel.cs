using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class fenceModel
    {
        public int fenceId { get; set; }
        public Nullable<int> vendorId { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        [DisplayName("Radius")]
        public int radius { get; set; }
        public string AreaName { get; set; }
        [DisplayName("Notification")]
        public string notification { get; set; }
        public System.DateTime startTime { get; set; }
        [DisplayName("Hours")]
        public int hr { get; set; }
        public System.DateTime endTime { get; set; }
        public virtual vendor vendor { get; set; }
        public int allowedFenceHours { get; set; }

        public string area { get; set; }
        public Nullable<int> adminId { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
    }
}