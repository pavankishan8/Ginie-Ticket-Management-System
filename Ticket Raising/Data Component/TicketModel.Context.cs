﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TicketEntities : DbContext
    {
        public TicketEntities()
            : base("name=TicketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<tktDepartment> tktDepartments { get; set; }
        public virtual DbSet<tktEmployee> tktEmployees { get; set; }
    }
}
