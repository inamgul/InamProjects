using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class messageModel
    {
        public int messageId { get; set; }
        public Nullable<int> vendorID { get; set; }
        public Nullable<int> adminID { get; set; }
        [DisplayName("Description")]
        public string messageDescription { get; set; }
        public int areaId { get; set; }
        public string messageStatus { get; set; }
        [DisplayName("Title")]
        public string messageTitle { get; set; }
        [NotMapped]
        public List<areaAssignedModel> detail { get; set; }
        [NotMapped]
        [DisplayName("Area Name")]
        public List<string> areaName { get; set; }
        public virtual area area { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
        public virtual vendor vendor { get; set; }
    }
}