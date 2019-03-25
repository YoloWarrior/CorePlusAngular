using Api.Models;
using CorePlusAngular.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;

namespace CorePlusAngular.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {

            
           
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Value> Values { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>();
            
            modelBuilder.Entity<Value>();
         
            base.OnModelCreating(modelBuilder);
        }   
                
    }
}
