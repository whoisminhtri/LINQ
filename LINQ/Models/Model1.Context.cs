﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LINQ.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PracticeSQLEntities : DbContext
    {
        public PracticeSQLEntities()
            : base("name=PracticeSQLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DMDIEMHP> DMDIEMHPs { get; set; }
        public virtual DbSet<DMHOCPHAN> DMHOCPHANs { get; set; }
        public virtual DbSet<DMKHOA> DMKHOAs { get; set; }
        public virtual DbSet<DMLOP> DMLOPs { get; set; }
        public virtual DbSet<DMNGANH> DMNGANHs { get; set; }
        public virtual DbSet<DMSINHVIEN> DMSINHVIENs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}