using System;
using Blog.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext()
            :base("name=BlogConnection")
        {

        }

        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove plural Table Name when generate database
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Category fields
            modelBuilder.Entity<Category>()
                .HasKey(c => c.category_id);

            modelBuilder.Entity<Category>().Property(c => c.category_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(0);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().Property(c => c.category_name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnOrder(1);

            modelBuilder.Entity<Category>().Property(c => c.category_slug)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnOrder(2);

            modelBuilder.Entity<Category>().Property(c => c.category_description)
                .IsMaxLength()
                .HasColumnOrder(3);

            // User fields
            modelBuilder.Entity<User>()
                .HasKey(u => u.id);

            modelBuilder.Entity<User>().Property(u => u.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnOrder(0);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.user_username)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnOrder(1);

            modelBuilder.Entity<User>().Property(u => u.user_password)
                .HasMaxLength(64)
                .IsRequired()
                .HasColumnOrder(2);

            modelBuilder.Entity<User>().Property(u => u.user_email)
                .HasMaxLength(256)
                .IsRequired()
                .HasColumnOrder(3);

            modelBuilder.Entity<User>().Property(u => u.user_firstname)
                .HasMaxLength(50)
                .HasColumnOrder(4);

            modelBuilder.Entity<User>().Property(u => u.user_lastname)
                .HasMaxLength(50)
                .HasColumnOrder(5);

            modelBuilder.Entity<User>().Property(u => u.user_lastname)
               .HasMaxLength(50)
               .HasColumnOrder(6);

            modelBuilder.Entity<User>().Property(u => u.user_avatar)
                .IsMaxLength()
                .HasColumnOrder(7);

        }
    }
}
