﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eGIS_Management.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IMTS_GISEntities : DbContext
    {
        public IMTS_GISEntities()
            : base("name=IMTS_GISEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<GIS_Environment> GIS_Environment { get; set; }
        public virtual DbSet<GIS_Server> GIS_Server { get; set; }
        public virtual DbSet<Network_Zone> Network_Zone { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<OS> OS { get; set; }
    }
}