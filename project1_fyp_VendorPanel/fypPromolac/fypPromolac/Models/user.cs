using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace fypPromolac.Models
{

    public class user
    {
        
        public int userId { get; set; }
        public string userName { get; set; }

    
        [Required]
        public string password { get; set; }

        public string userRole { get; set; }
        public string userAdmin { get; set; }

        public Nullable<int> m_Allowed { get; set; }
        public Nullable<int> m_sent { get; set; }

        [NotMapped]
        public List<area> detail { get; set; }
        
       
        [Required]
        public string Newpassword { get; set; }
        [NotMapped]
        [Compare("Newpassword", ErrorMessage = "Password doesn't match.")]
        
        public string cnfrmpassword { get; set; }




        public virtual ICollection<area> area { get; set; }
    }
}