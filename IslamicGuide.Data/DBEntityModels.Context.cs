﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IslamicGuide.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A4DE6E_IslamicGuideEntities : DbContext
    {
        public DB_A4DE6E_IslamicGuideEntities()
            : base("name=DB_A4DE6E_IslamicGuideEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookContent> BookContents { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<MapBookQuran> MapBookQurans { get; set; }
        public virtual DbSet<MapSubjectsQuran> MapSubjectsQurans { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<QuranAyat> QuranAyats { get; set; }
        public virtual DbSet<QuranSuar> QuranSuars { get; set; }
        public virtual DbSet<QuranWord> QuranWords { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
