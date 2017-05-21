using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using StorageLayer.Model;

namespace StorageLayer.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class PermutationContext : DbContext
    {
        public DbSet<LagePermutation> LargePermutations { get; set; }

        private static string ConnectionString = "server=nas35.muncic.local;port=48111;database=large_permutation_store;uid=perm_store;Pwd=perm_store";

        public PermutationContext() 
            : base(ConnectionString) { }
    }
}
