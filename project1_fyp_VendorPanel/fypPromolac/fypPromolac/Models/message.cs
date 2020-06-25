using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace fypPromolac.Models
{
    public partial class message
    {
        public int messageid { get; set; }
        public int m_allowed { get; set; }
        public int m_sent { get; set; }

        [NotMapped]
        public int m_remaining { get; set; }
        [NotMapped]
        public int m_remaining1 { get; set; }
    }

}