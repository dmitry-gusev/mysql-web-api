using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DAL
{
    public class MySqlDbContext:DbContext
    {
        /// <summary>
        /// Products table
        /// </summary>
        public DbSet<Product> Products { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> option) : base(option)
        {

        }

        
        
    }
}
