using Microsoft.EntityFrameworkCore;
using Paging.API.Models;

namespace Paging.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
        public DbSet<Customer> customers { get; set; }
    }
}
