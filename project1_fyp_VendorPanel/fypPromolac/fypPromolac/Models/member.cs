using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public partial class member
    {

        [DisplayName("Member Id:")]
        public int memberID { get; set; }
        [DisplayName("Member Name:")]
        public string m_name { get; set; }
        [DisplayName("UserName:")]
        public string m_userName { get; set; }
        [DisplayName("Password:")]
        public string m_password { get; set; }
        [DisplayName("Role:")]
        public string m_role { get; set; }
        public Nullable<int> m_admin { get; set; }
    }
}