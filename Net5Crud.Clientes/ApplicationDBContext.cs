using Microsoft.EntityFrameworkCore;
using Net5Crud.Clientes.Entities;

namespace Net5Crud.Clientes
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {
        }
        //public ApplicationDBContext(DbContextOptions options) : base(options)
        //{
        //  //  Database.Migrate();
        //}
        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Usuario> Usuario { get; set; }

    }
}
