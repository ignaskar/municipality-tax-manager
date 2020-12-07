using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaxManager.Core.Entities;

namespace TaxManager.Infra.Data
{
    public class TaxManagerContext : DbContext
    {
        public TaxManagerContext(DbContextOptions<TaxManagerContext> options) : base (options)
        {
            
        }

        public DbSet<Municipality> Municipalities { get; set; }
    }
}