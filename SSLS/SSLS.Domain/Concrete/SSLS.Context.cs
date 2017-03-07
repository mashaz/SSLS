﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSLS.Domain.Concrete
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class SSLSEntities : DbContext
    {
        public SSLSEntities()
            : base("name=SSLSEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Borrow> Borrow { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Fine> Fine { get; set; }
        public virtual DbSet<Reader> Reader { get; set; }
    }
}
