using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace fypPromolac.Models
{
    public class userPackageModel
    {
        public int vendorId { get; set; }
        public int packageId { get; set; }
        public System.DateTime packageStartTime { get; set; }
        public int dividedShareOfMessages { get; set; }
        public string packageStatus { get; set; }
        public System.DateTime packageEndTime { get; set; }
        [NotMapped]
        public int messagesAllowed { get; set; }
        [NotMapped]
        public int messagesRemaining { get; set; }
        public string userName { get; set; }
        public int notificationSent { get; set; }
        public Nullable<int> remainingFencingHours { get; set; }
        public List<areaAssignedModel> detail { get; set; }
        public int fenceCreated { get; set; }
        public int dealsCreated { get; set; }

        public virtual package package { get; set; }
        public virtual vendor vendor { get; set; }
    }
}