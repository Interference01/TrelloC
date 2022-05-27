﻿using Microsoft.EntityFrameworkCore;
using TrelloC.Models.Entities;

namespace TrelloC.Core
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<User> Users { get; set; }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}
