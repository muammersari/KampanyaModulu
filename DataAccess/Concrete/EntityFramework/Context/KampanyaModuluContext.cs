using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class KampanyaModuluContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MUAMMER\SQLEXPRESS; Database=KampanyaModuluDb; uid=sa; pwd=ms14219019*;");
        }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Campaing> Campaings { get; set; }
        public DbSet<CampaingAndProduct> CampaingAndProducts { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
