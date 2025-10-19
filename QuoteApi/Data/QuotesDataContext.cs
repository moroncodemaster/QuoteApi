using Microsoft.EntityFrameworkCore;
using QuoteApi.Models;

namespace QuoteApi.Data;

public class QuotesDataContext : DbContext
{
    private readonly string _connectionString;
#if DEBUG
    public QuotesDataContext()
    {
        _connectionString = "Host=10.0.10.10;Port=8432;Database=quotes;Username=dbutt;Password=M3andsara";
    }
    // public QuotesDataContext(DbContextOptions<QuotesDataContext> options) : base(options)
    // {
    //     _connectionString = "Host=10.0.10.10;Port=8432;Database=quotes;Username=dbutt;Password=M3andsara";
    // }

#endif
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