﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace serviciosrest
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bdtienditaEntities : DbContext
    {
        public bdtienditaEntities()
            : base("name=bdtienditaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<distrito> distrito { get; set; }
        public virtual DbSet<empleado> empleado { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<rol> rol { get; set; }
    }
}
