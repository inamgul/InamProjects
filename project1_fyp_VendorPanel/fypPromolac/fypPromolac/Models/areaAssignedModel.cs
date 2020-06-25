using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fypPromolac.Models
{
    public class areaAssignedModel
    {
        [Key, Column(Order = 0)]
        public int vendorId { get; set; }
        [Key, Column(Order = 1)]
        public int areaId { get; set; }
        [Key]
        public int assignedId { get; set; }

        public virtual area area { get; set; }
        public virtual vendor vendor { get; set; }

        public List<string> areas { get; set; }
        public string areaName { get; set; }
        //public List<areaModel> detail { get; set; }

        [NotMapped]
        public List<areaAssignedModel> detail { get; set; }
        [NotMapped]
        public int subUserId { get; set; }
        public int check(int subUserId)
        {
            if (subUserId == 1)
            {
                subUserId = 2;
                return subUserId;
            }
            else
                return 0;
        }
    }
}