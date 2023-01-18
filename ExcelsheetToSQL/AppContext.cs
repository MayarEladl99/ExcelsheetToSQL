using Microsoft.EntityFrameworkCore;

namespace ExcelsheetToSQL;

public class AppContext: DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }

    
  


    public DbSet<workers> Workers { get; set; }
}