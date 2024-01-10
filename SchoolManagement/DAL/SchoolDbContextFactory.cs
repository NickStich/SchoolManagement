using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolManagement.DAL;

public class SchoolDbContextFactory : IDesignTimeDbContextFactory<SchoolDbContext>
{
    public SchoolDbContext CreateDbContext(string[] args)
    {
        var optionsBulder = new DbContextOptionsBuilder<SchoolDbContext>();
        optionsBulder.UseSqlServer(@"Data source=ROMOB41160\SQL2014;initial catalog=SchoolManagement;trusted_connection=true;TrustServerCertificate=True");

        return new SchoolDbContext(optionsBulder.Options);
    }
}
