using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace fypPromolac.Models
{
    public class vendorModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vendorModel()
        {
            this.areaAssigneds = new HashSet<areaAssigned>();
            this.deals = new HashSet<deal>();
            this.userPackages = new HashSet<userPackage>();
            this.vendor1 = new HashSet<vendor>();
        }
        [Key]
        public int vendorId { get; set; }
        [DisplayName("First sName")]
        [Required(ErrorMessage = "First name is required")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string lastName { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(11)]
        [MinLength(11)]
        public string phoneNumber { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string vendorEmail { get; set; }
        [DisplayName("Company Address")]
        [Required(ErrorMessage = "Address is required")]
        public string vendorAddress { get; set; }
        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company Name is required")]
        public string vendorCompanyName { get; set; }
        public System.DateTime registerTimestamp { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required")]
        [Remote("IsUserNameAvailable", "userInfoes",
                ErrorMessage = " Username already exists")]
        public string vendorUserName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required")]
        public string vendorPassword { get; set; }
        public string vendorStatus { get; set; }
        public Nullable<int> vendorAdminId { get; set; }
        public Nullable<int> headId { get; set; }
        public string isHead { get; set; }
        [NotMapped]
        public List<areaAssignedModel> detail { get; set; }
        public int messagesAllowed { get; set; }
        public int fencingHours { get; set; }
        public int fencingHoursStock { get; set; }

        public int messagesStock { get; set; }

        public List<string> areas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<areaAssigned> areaAssigneds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<deal> deals { get; set; }
        public virtual mainAdmin mainAdmin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userPackage> userPackages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vendor> vendor1 { get; set; }
        public virtual vendor vendor2 { get; set; }

    }
}