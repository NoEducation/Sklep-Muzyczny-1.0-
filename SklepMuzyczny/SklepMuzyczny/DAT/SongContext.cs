using SklepMuzyczny.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SklepMuzyczny.DAT
{
    public class SongContext : DbContext
    {
        public SongContext() : base("SongContext")
        {
           // Database.SetInitializer<SongContext>(new SongInitializer());
        }
        
        static SongContext()
        {
            Database.SetInitializer<SongContext>(new SongInitializer());
        }
        
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<Category> Categories { get; set; } 
        public virtual DbSet<Order> Orders { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // disable conventions, which  gives s to end of field
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}