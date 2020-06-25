using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
   public partial class area
    {
       
        public int areaId { get; set; }
        public string areaName { get; set; }

        public int Id { get; set; }
        public virtual user user { get; set; }

        [NotMapped]
        public List<area> detail { get; set; }

    }
}