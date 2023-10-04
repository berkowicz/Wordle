using Microsoft.EntityFrameworkCore;

namespace Wordle.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
