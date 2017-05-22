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

namespace StorageLayer.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class PermutationContext : DbContext
    {
        public DbSet<LargePermutation> LargePermutations { get; set; }

        private static string ConnectionString = "server=nas35.muncic.local;port=48111;database=large_permutation_store;uid=perm_store;Pwd=perm_store";

        public PermutationContext() 
            : base(ConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LargePermutation>()
                .HasKey(key => key.Permutation)
                .ToTable($"{nameof(LargePermutation)}s")
                .Property(p => p.Permutation)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                        GenerateUniqueConstraint<LargePermutation>("Permutation"))
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }

        private IndexAnnotation GenerateUniqueConstraint<TEntity>(string propertyName)
        {
            Random random = new Random();
            byte[] randomBytes = new byte[1000];
            random.NextBytes(randomBytes);
            // 90 - 108
            char[] randomSuffix = randomBytes.Where(b => b >= 65 && b <= 90)
                .Select(b => Convert.ToChar(b))
                .ToArray();
            if (randomSuffix.Length > 5)
            {
                randomSuffix = randomSuffix.Take(5).ToArray();
            }
            string randomSuffixAsString = randomSuffix.Aggregate("", (a, b) => a + Convert.ToString(b));
            string uniqueConstraintName = $"UX_{typeof(TEntity).Name}_{propertyName}_{randomSuffixAsString}";
            IndexAttribute newIndexAttribute = new IndexAttribute(uniqueConstraintName){IsUnique = true};
            IndexAnnotation newIndexAnnotation = new IndexAnnotation(newIndexAttribute);
            return newIndexAnnotation;
        }
    }
}
