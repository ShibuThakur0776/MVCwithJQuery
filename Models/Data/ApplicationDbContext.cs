﻿using Microsoft.EntityFrameworkCore;

namespace MVCwithJQuery.Models.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
