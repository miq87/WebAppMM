using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WebAppMM.Models;

namespace WebAppMM.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}
