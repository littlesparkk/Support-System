//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asp.NETMVCCRUD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminSection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminSection()
        {
            this.UserComments = new HashSet<UserComment>();
        }
    
        public int AdminSectionId { get; set; }
        public string Ticket_No { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public string Priority { get; set; }
        public string Raised_By { get; set; }
        public Nullable<System.DateTime> Raised_On { get; set; }
        public Nullable<System.DateTime> Due_On { get; set; }
        public Nullable<System.DateTime> Resolved_On { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserComment> UserComments { get; set; }
    }
}