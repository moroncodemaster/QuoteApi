using Microsoft.EntityFrameworkCore;
using QuoteApi.Models;

namespace QuoteApi.Data;

public class QuotesDataContext : DbContext
{
    private readonly string _connectionString;
    public QuotesDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString: _connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public void SetModified(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }

    public void SetDetached(object entity)
    {
        Entry(entity).State = EntityState.Detached;
    }

    public void MigrateContext()
    {
        //var config = new Configuration
        this.Database.Migrate();

    }

    public DbSet<Quote> Quotes { get; set; }

}