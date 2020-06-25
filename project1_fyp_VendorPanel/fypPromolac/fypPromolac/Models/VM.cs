using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class VM
    {
        public int notificationId { get; set; }
        public string notificationTitle { get; set; }
        public string notificationImage { get; set; }
        public string notificationDescription { get; set; }

        public int areaId { get; set; }
        public string areaName { get; set; }

       // public virtual userInfo userInfo { get; set; }
        public List<area> detail { get; set; }
    }
}