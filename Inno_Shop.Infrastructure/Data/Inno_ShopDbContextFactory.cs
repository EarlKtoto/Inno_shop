using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Inno_Shop.Infrastructure.Data;

public class Inno_ShopDbContextFactory : IDesignTimeDbContextFactory<Inno_ShopDbContext>
{
    public Inno_ShopDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Inno_ShopDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ShopDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new Inno_ShopDbContext(optionsBuilder.Options);
    }
}


