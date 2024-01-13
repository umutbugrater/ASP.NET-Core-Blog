using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-JCPO54A\\SQLEXPRESS; database=BlogAppDb; integrated security=true;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(x => x.MessageReceiver)
                .HasForeignKey(x => x.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Message>()
                .HasOne(x=>x.SenderUser)
                .WithMany(x=>x.MessageSender)
                .HasForeignKey(x=>x.SenderID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(builder);
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
