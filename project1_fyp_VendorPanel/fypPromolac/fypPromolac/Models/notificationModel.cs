using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class notificationModel
    {
        [Key]
        public string notificationId { get; set; }
        [DisplayName("Title")]
        public string notificationTitle { get; set; }
        [DisplayName("Description")]
        public string notificationDescription { get; set; }
        public string notificationStatus { get; set; }
        public Nullable<int> vendorId { get; set; }
        public Nullable<int> adminId { get; set; }
        public string notificationAreaId { get; set; }
        public DateTime notificationTime { get; set; }
        [NotMapped]
        public List<string> areaName { get; set; }
        public string areas { get; set; }
        [NotMapped]
        public string userName { get; set; }
        [NotMapped]
        public List<areaAssignedModel> detail { get; set; }
        public virtual area area { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
        public virtual vendor vendor { get; set; }
    }
}