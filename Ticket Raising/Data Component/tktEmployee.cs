//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketRaising.Data_Component
{
    using System;
    using System.Collections.Generic;
    
    public partial class tktEmployee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tktEmployee()
        {
            this.Tickets = new HashSet<Ticket>();
            this.Tickets1 = new HashSet<Ticket>();
        }
    
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DeptId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets1 { get; set; }
        public virtual tktDepartment tktDepartment { get; set; }
    }
}