using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.Entity;

using StorageLayer.Model;

using System.Data.Entity.Infrastructure.Annotations;
using System.Data.SqlTypes;
using System.Security.Policy;


namespace StorageLayer.Context
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class PermutationContext : DbContext
    {

        public DbSet<LargePermutation> LargePermutations { get; set; }

        private static string ConnectionString =
            "server=nas35.muncic.local;port=48111;database=large_permutation_store;uid=perm_store;Pwd=perm_store";

        public PermutationContext()
            : base(ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LargePermutation>()
                        .HasKey(key => key.Permutation)
                        .ToTable($"{nameof(LargePermutation)}s")
                        .Property(p => p.Permutation)
                        .HasMaxLength(64)
                        .IsUnicode()
                        .IsRequired();

            modelBuilder.Entity<LargePermutation>()
                        .Property(p => p.BytePermutation)
                        .IsRequired()
                        .IsMaxLength()
                        .HasColumnType("blob");

            base.OnModelCreating(modelBuilder);
        }

    }


    public static class PermutationContextFactory
    {

        public static PermutationContext NewContext => CreateNewPermutationContext();

        private static PermutationContext CreateNewPermutationContext()
        {
            return new PermutationContext();
        }

        private static PermutationContext CreateNewSlimPermutationContext()
        {
            PermutationContext ctx = new PermutationContext();
            return ctx;
        }

    }

}
