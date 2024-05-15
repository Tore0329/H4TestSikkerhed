using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace H4TestSikkerhedApp.Data
{
    public class ApplicationMockDbContext(DbContextOptions<ApplicationMockDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}