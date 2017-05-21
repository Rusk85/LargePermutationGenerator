using System.Data.Entity;
using StorageLayer.Context;

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
            }
        }



    }
}
