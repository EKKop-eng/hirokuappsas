using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuprynas.Models;
using Microsoft.EntityFrameworkCore;

namespace Kuprynas.Data
{
    public class KruprynasDbContext : DbContext
    {
        public KruprynasDbContext(DbContextOptions<KruprynasDbContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryItem> GroceryItems { get; set; } = default!;
    }
}
