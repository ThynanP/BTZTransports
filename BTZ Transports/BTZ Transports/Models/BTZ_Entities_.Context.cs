﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTZ_Transports.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BTZ_Entities_ : DbContext
    {
        public BTZ_Entities_()
            : base("name=BTZ_Entities_")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TbAbastecimentos> TbAbastecimentos { get; set; }
        public virtual DbSet<TbCarro> TbCarro { get; set; }
        public virtual DbSet<TbMotorista> TbMotorista { get; set; }
        public virtual DbSet<TbTipoCombustivel> TbTipoCombustivel { get; set; }
        public virtual DbSet<TbTipoFabricante> TbTipoFabricante { get; set; }
        public virtual DbSet<TbTipoStatus> TbTipoStatus { get; set; }
        public virtual DbSet<TbTipoStatusCar> TbTipoStatusCar { get; set; }
        public virtual DbSet<TbTipoUser> TbTipoUser { get; set; }
        public virtual DbSet<tbUser> tbUser { get; set; }
    }
}
