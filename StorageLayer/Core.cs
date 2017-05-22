using System.Data.Entity;
using StorageLayer.Context;
using StorageLayer.Model;

namespace StorageLayer
{
    public class Core
    {

        public void UpradeDatabase()
        {
            using (PermutationContext pc = new PermutationContext())
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<PermutationContext>());
                pc.Database.Initialize(true);


                var largePermutation = new LargePermutation
                {
                    Permutation = "ABCDEFG"
                };

                pc.LargePermutations.Add(largePermutation);
                pc.SaveChanges();
                pc.LargePermutations.Add(largePermutation);
                pc.SaveChanges();


            }
        }



    }
}
