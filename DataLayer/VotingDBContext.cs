using BusinessLayer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VotingDBContext : IdentityDbContext<User>
    {
        public VotingDBContext() : base()
        {

        }
        public VotingDBContext(DbContextOptions options) : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          //  optionsBuilder.UseSqlServer(@"Server = localhost\SQLEXPRESS; Database=EVotingFace; Trusted_Connection=True;TrustServerCertificate=True;");
            //optionsBuilder.UseSqlServer(@" Server = localhost\SQLEXPRESS; Database = EVoting7; Trusted_Connection = True; MultipleActiveResultSets=true;");            
                
               
  
        //base.OnConfiguring(optionsBuilder);   
         optionsBuilder.UseInMemoryDatabase("Test");  
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IDCardPhotos>()
                .Property(vd => vd.IDCardImage1).HasColumnType("varbinary(max)");


            modelBuilder.Entity<IDCardPhotos>()
               .Property(vd => vd.IDCardImage2).HasColumnType("varbinary(max)");

            modelBuilder.Entity<Party>()
              .HasIndex(p => p.Name)
              .IsUnique();

            modelBuilder.Entity<Answer>()
            .HasOne(a => a.Candidate)
            .WithMany()
            .HasForeignKey(a => a.CandidateID)
            .OnDelete(DeleteBehavior.SetNull);



            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<IDCard> IDCards { get; set; }
        public DbSet<IDCardPhotos> IDCardsPhotos { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Question> Questions { get; set; }

    }
}
