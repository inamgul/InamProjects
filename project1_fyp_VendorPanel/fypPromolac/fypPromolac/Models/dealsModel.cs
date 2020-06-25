using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class dealsModel
    {
        [JsonIgnore]
        public int dealsId { get; set; }
        [JsonIgnore]
        [DisplayName("Name")]
        public string dealName { get; set; }
        [DisplayName("Price")]
        public double dealPrice { get; set; }
        [DisplayName("Category")]
        [JsonIgnore]
            public string dealCategory { get; set; }
        [JsonIgnore]
        public int vendorId { get; set; }
        public double longitude { get; set; }
            public double latitude { get; set; }
        [DisplayName("Description")]
        public string dealDescription { get; set; }
            public string AreaName { get; set; }
        [DisplayName("Image")]
        public string dealImage { get; set; }
        public string area { get; set; }
        public int adminId { get; set; }
        public List<dealCategory> dealCategoryLists { get; set; }

            public virtual vendor vendor { get; set; }
        
    }
}