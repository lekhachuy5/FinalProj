﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClockUniverse
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClockUniverseEntities : DbContext
    {
        public ClockUniverseEntities()
            : base("name=ClockUniverseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<QLdonHang> QLdonHangs { get; set; }
        public virtual DbSet<QLTT> QLTTs { get; set; }
        public virtual DbSet<QuanLyDH> QuanLyDHs { get; set; }
    }
}